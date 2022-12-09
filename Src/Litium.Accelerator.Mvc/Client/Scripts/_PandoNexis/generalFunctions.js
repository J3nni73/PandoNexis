
/**
 * finds the object with the given id in the given structure
 * @param obj the structure
 * @param search the identity of the object to find
 * @param id an optional parent
 * @param parent an optional parent
 */
export const lookup = (obj, search, idName="id", parent) => {
    const searchId = search.toString();
    const objId = obj[idName] !== undefined ? obj[idName].toString() : -1;
    if (parent)
        obj.parent = parent;
    if (objId === searchId) {
        return obj;
    } else if (obj.children) {
        for (let x = 0; x < obj.children.length; x++) {
            const child = lookup(obj.children[x], searchId, obj);
            const childId = child.id !== undefined ? child.id.toString() : -1;
            if (childId === searchId) {
                return child;
            }
        }
    }
    return {};
}

const traverse = (obj, identities) => {
    if (obj) {
        identities.unshift(obj.id);
        traverse(obj.parent, identities);
    }
}