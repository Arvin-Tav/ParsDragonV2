
ClassicEditor
    .create(document.querySelector('#Body'), {
        toolbar: {
            items: [
                'heading',
                '|',
                'bold',
                'italic',
                'link',
                '|',
                'fontSize',
                'fontColor',
                '|',
                'imageUpload',
                'blockQuote',
                'insertTable',
                'undo',
                'redo',
                'codeBlock'
            ]
        },
        language: 'fa',
        table: {
            contentToolbar: [
                'tableColumn',
                'tableRow',
                'mergeTableCells'
            ]
        },
        licenseKey: '',
        ckfinder: { uploadUrl: '/file-upload' },

    })
    .then(editor => {
        window.editor = editor;




    })
    .catch(error => {
        console.error(error);
    });