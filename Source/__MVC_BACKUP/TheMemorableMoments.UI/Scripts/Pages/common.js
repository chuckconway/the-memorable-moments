function showMessage(message) {

    $('span.message').text(message);

    if(message){
        $('span.message').jnotifica({
            position: 'top',
            background: '#ECCC6A',
            effect: 'fade',
            opacity: '0.95',
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
    
    sidebar.hide();
    sidebar.removeClass("hidden");
    sidebar.fadeIn("slow");
}

function attachImgPreview(path) {

    $(path).imgPreview({
        containerID: 'imgPreviewWithStyles',
        imgCSS: {
            // Limit preview size:
            height: 300,
            onShow: function () {
                $(this).fadeIn();
            }

        },
        // When container is shown:
        onShow: function (link) {
            var title = $(link).attr('title');
            if (title) {
                $('<span>' + title + '</span>').appendTo(this);
            }
        },
        // When container hides: 
        onHide: function (link) {
            $('span', this).remove();
        }
    });

}


function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}

String.prototype.splitCSV = function(sep) {
  for (var foo = this.split(sep = sep || ","), x = foo.length - 1, tl; x >= 0; x--) {
    if (foo[x].replace(/"\s+$/, '"').charAt(foo[x].length - 1) == '"') {
      if ((tl = foo[x].replace(/^\s+"/, '"')).length > 1 && tl.charAt(0) == '"') {
        foo[x] = foo[x].replace(/^\s*"|"\s*$/g, '').replace(/""/g, '"');
      } else if (x) {
        foo.splice(x - 1, 2, [foo[x - 1], foo[x]].join(sep));
      } else foo = foo.shift().split(sep).concat(foo);
    } else foo[x].replace(/""/g, '"');
  } return foo;
};


function remove() {
    var self = $(this);
    var hiddenField = $('input#selectedalbums');
    var id = self.parent().attr('id');
    self.parent().remove();

    var values = hiddenField.val().replace(id + ',', '');
    hiddenField.val(values);
    return false;
};