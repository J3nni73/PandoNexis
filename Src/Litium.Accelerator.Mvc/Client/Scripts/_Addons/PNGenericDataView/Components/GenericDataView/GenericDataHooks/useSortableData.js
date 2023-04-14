import React from 'react';

export const useSortableData = (items, config = null) => {
  const [sortConfig, setSortConfig] = React.useState(config);

  const collator = new Intl.Collator(undefined, {
    numeric: true,
    sensitivity: 'base',
  });
  const sortedItems = React.useMemo(() => {
    let sortableItems = [...items];
    if (sortConfig !== null) {
      const kkey = sortConfig?.key;
      // sortableItems.sort((x, y) => {
      //   let a = x.fields;
      //   let b = y.fields;
      //   if (
      //     a[sortConfig.index][sortConfig.key] <
      //     b[sortConfig.index][sortConfig.key]
      //   ) {
      //     return sortConfig.direction === 'ascending' ? -1 : 1;
      //   }
      //   if (
      //     a[sortConfig.index][sortConfig.key] >
      //     b[sortConfig.index][sortConfig.key]
      //   ) {
      //     return sortConfig.direction === 'ascending' ? 1 : -1;
      //   }

      //   return 0;
      // });
      // console.log('Collator ', collator);
      sortableItems.sort((x, y) => {
        let a = x.fields;
        let b = y.fields;
        if (sortConfig.direction === 'ascending') {
          return collator.compare(
            a[sortConfig.index][sortConfig.key],
            b[sortConfig.index][sortConfig.key]
          );
        }
        if (sortConfig.direction === 'descending') {
          return collator.compare(
            b[sortConfig.index][sortConfig.key],
            a[sortConfig.index][sortConfig.key]
          );
        }
      });
    }

    return sortableItems;
  }, [items, sortConfig]);

  const requestSort = (key, index, fieldName) => {
    let direction = 'ascending';
    if (
      sortConfig &&
      sortConfig.fieldName === fieldName &&
      sortConfig.direction === 'ascending'
    ) {
      direction = 'descending';
    }
    setSortConfig({ key, index, fieldName, direction });
  };

  return { items: sortedItems, requestSort, sortConfig };
};
//test
