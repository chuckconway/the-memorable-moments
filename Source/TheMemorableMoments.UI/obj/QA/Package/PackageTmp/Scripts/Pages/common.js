function showMessage(message) {

    $('span.message').text(message);

    if(message){
        $('span.message').jnotifica({
            position: 'top',
            background: '#ECCC6A',
            effect: 'fade',
            clickClose: true,
            timeout: 30000,
            color: '#000',
            cursor: 'default',
            msgCss: 
                {
                    fontSize: '13px',
                    fontFamily: 'Arial, sans-serif',
                    textAlign: 'center',
                    padding:'5px'
                            
                },
        });
    }
}



function fadeIn(name) {

    var sidebar = $(name);

    sidebar.removeClass("hidden");
    sidebar.hide();
    sidebar.fadeIn("slow");
}