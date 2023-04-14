import React from 'react';

const PaginationComponent = ({ postsPerPage, totalPosts, paginate }) => {
  const [number, setNumber] = React.useState(1);
  const totalPages = Math.ceil(totalPosts / postsPerPage);

  const pageNumbers = [];
  for (let i = 1; i <= Math.ceil(totalPosts / postsPerPage); i++) {
    pageNumbers.push(i);
  }

  const handleClick = (num) => {
    setNumber(num);
    paginate(num);
  };
  const previousPage = () => {
    const prev = number - 1;
    if (prev >= 1) {
      setNumber(prev);
      paginate(prev);
    }
  };
  const nextPage = () => {
    const next = number + 1;
    if (next <= totalPages) {
      setNumber(next);
      paginate(next);
    }
  };
  return (
    <div>
      <nav aria-label="Pagination">
        <ul className="pagination">
          <li className={`pagination-previous`}>
            <a
              href="#"
              aria-label="Previous page"
              onClick={() => previousPage()}
              className={` ${number === 1 ? 'disabled' : ''}`}
            >
              Previous <span className="show-for-sr">page</span>
            </a>
          </li>
          {pageNumbers.map((pageNum) => (
            <li
              key={pageNum}
              className={`${pageNum === number ? 'current' : ''}`}
            >
              {/* <span className="show-for-sr">You're on page</span> 1 */}
              <a
                href="#"
                aria-label={`Page ${pageNum}`}
                onClick={() => handleClick(pageNum)}
              >
                {pageNum}
              </a>
            </li>
          ))}

          {/* <li className="ellipsis" aria-hidden="true"></li>*/}

          <li className={`pagination-next`}>
            <a
              href="#"
              aria-label="Next page"
              onClick={() => nextPage()}
              className={` ${number === totalPages ? 'disabled' : ''}`}
            >
              Next <span className="show-for-sr">page</span>
            </a>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default PaginationComponent;
