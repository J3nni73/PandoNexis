/**
 * Parse location search params into an object with keys mapped to param names
 * @example
 * when location.search = ?countryOfOrigin=SE&countryOfOrigin=NL&partWeightPackage=0&sortBy=Z-A
 * returns {countryOfOrigin: ['SE', 'NL'], partWeightPackage: '0', sortBy: 'Z-A'}
 */
export const getURLSearchParams = (separator = ',') => {
    const params = new URLSearchParams(location.search);
    return [...new Set(params.keys())].reduce((values, key) => {
        const value = params.get(key);
        return {
            ...values,
            [key]:
                value && value.indexOf(separator) > 0
                    ? value.split(separator)
                    : value,
        };
    }, {});
};

export const buildURLSearchParams = (values = {}) => {
    const params = new URLSearchParams(location.search);
    Object.keys(values).forEach((key) => {
        const value = Array.isArray(values[key])
            ? values[key].join(',')
            : values[key];
        if (value) {
            params.set(key, value);
        } else {
            params.delete(key);
        }
    });
    return Boolean(params.toString())
        ? `?${params.toString().replace(/%2C/g, ',')}`
        : '';
};
