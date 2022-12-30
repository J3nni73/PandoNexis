import React, { useState } from 'react';
import { useCallback } from 'react';
import { useDispatch } from 'react-redux';
import SweetAlert2, { withSwal } from 'react-sweetalert2';
import { removeArticleFromOuotaCategory, removeArticlesTempListForAdd } from '../../../Actions/Quota/QuotaQuickSearch.action';


const RemoveAllArticlesComponent = ({ items = [], type, setRemoveData }) => {

    const dispatch = useDispatch();
    const [show, setShow] = useState(false)
    const [swalProps2, setSwalProps2] = useState({
        show: true,
        title: 'Deleted!',
        text: 'Your file has been deleted.',
        icon: 'success',
        timer: 1000,
        showCancelButton: false,
        showConfirmButton: false
    });
    const [swalProps, setSwalProps] = useState();

    const handleClick = (e) => {
        e.preventDefault();
        setSwalProps({
            show: true,
            title: `${items.length} Selected Articles`,
            text: 'Want to delete article number?',
            showCancelButton: true,
            // showDenyButton: true,
            // denyButtonText: 'Cancel',
            confirmButtonText: 'OK',
            position: 'top',
            didOpen: () => {
                // run when swal is opened...
            },
            didClose: () => {
                // run when swal is closed...
                // setShow(false)

            },
            onConfirm: (result) => {
                // run when clieked in confirm and promise is resolved...
                if (result.isConfirmed) {
                    setShow(true);
                    setTimeout(() => {
                        dispatch(removeArticlesTempListForAdd(items));
                        setRemoveData([]);
                    }, 1200);
                }

            },
            onError: (error) => {
                // run when promise rejected...
            },
            onResolve: (result) => {
                // run when promise is resolved...
                setSwalProps({})
            }
        });
    }

    return (
        <div>
            <button className="button button-orange article-numbers__remove-all-btn"
                name="removeArticle" onClick={handleClick} >
                Remove All checked
            </button>
            {show && <SweetAlert2 {...swalProps2} />}
            <SweetAlert2 {...swalProps}>
                <ul>
                    {items && items.length > 0 &&
                        items.map((item, index) => {
                            return <li key={index}> {item.articleNumber} </li>
                        })}
                </ul>
            </SweetAlert2>
        </div>
    );
}

export default RemoveAllArticlesComponent