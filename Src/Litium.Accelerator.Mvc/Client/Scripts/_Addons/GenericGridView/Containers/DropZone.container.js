import React, { Fragment, useEffect } from 'react'
import { connect, useDispatch, useSelector } from 'react-redux'
import { uploadDropZoneFile } from '../Actions/DropZone.action';

// const DropZoneContainer = () => {
//     const dispatch = useDispatch();


//     useEffect(() => {
//         //let main = document.querySelectorAll('.main-content')[0];
//         ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
//             document.addEventListener(eventName, (event) => {
//                 this.preventDefaults(event);
//             });
//         });

//         ['dragenter', 'dragover'].forEach(eventName => {
//             document.addEventListener(eventName, (event) => {
//                 this.highlight(event);
//             });
//         });

//         ['dragleave', 'drop'].forEach(eventName => {
//             document.addEventListener(eventName, (event) => {
//                 this.unhighlight(event);
//             });
//         });
//         document.addEventListener('drop', (event) => {
//             this.handleDrop(event);
//         });

//         //   return () => {
//         //     second
//         //   }
//     }, [])


//     function highlight(e) {
//         //let main = document.querySelectorAll('.main-content')[0];
//         //main.classList.add("highlight");
//     }

//     function unhighlight(e) {
//         // let main = document.querySelectorAll('.main-content')[0];
//         //main.classList.remove("highlight");
//     }

//     function handleDrop(e) {
//         let dt = e.dataTransfer;
//         let files = ([...dt.files]);
//         for (var i = 0; i < files.length; i++) {
//             dispatch(uploadDropZoneFile(file[i], type))
//         }
//     }

//     return (
//         <Fragment></Fragment>
//     )
// }

// export default DropZoneContainer


class DropZoneContainer extends React.Component {

    componentDidMount() {

        //let main = document.querySelectorAll('.main-content')[0];
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {

            document.addEventListener(eventName, (event) => {
                this.preventDefaults(event);
            });
        });

        ['dragenter', 'dragover'].forEach(eventName => {
            document.addEventListener(eventName, (event) => {
                this.highlight(event);
            });
        });

        ['dragleave', 'drop'].forEach(eventName => {
            document.addEventListener(eventName, (event) => {
                this.unhighlight(event);
            });
        });
        document.addEventListener('drop', (event) => {
            console.log('dropZone ', event);
            this.handleDrop(event);
        });
    }

    render() {
        return (<Fragment></Fragment>);
    }

    highlight(e) {
        //let main = document.querySelectorAll('.main-content')[0];
        //main.classList.add("highlight");
    }

    unhighlight(e) {
        // let main = document.querySelectorAll('.main-content')[0];
        //main.classList.remove("highlight");
    }

    handleDrop(e) {
        let dt = e.dataTransfer;
        let files = ([...dt.files]);


        for (var i = 0; i < files.length; i++) {
            this.props.uploadDropZoneFile(files[i], this.props.type)
        }
    }

    preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    componentWillUnmount() {

    }

    componentWillUpdate() {

    }

}


const mapStateToProps = state => {
    const { dropZone } = state;

    return {
        ...dropZone,
    }
}

const mapDispatchToProps = dispatch => {
    return {
        uploadDropZoneFile: (file, type) => dispatch(uploadDropZoneFile(file, type)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(DropZoneContainer);
