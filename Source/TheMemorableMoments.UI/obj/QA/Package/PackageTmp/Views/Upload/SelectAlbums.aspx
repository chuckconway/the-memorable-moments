<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div style="width: 200px; border-bottom: 1px dashed rgb(0, 0, 0); height: 10px;margin-bottom:15px"></div>
 <p style="font-size:11px;" >To add an album click on the album name. An album can only be selected once.</p>
    <label  style="font-size:11px; margin-top:30px;">Selected Albums</label>    
    <ul style="font-size:10px; width:200px;margin-top:10px;" id="albums" ></ul>
    <%= Html.Hidden("selectedalbums") %>

    <div id="albumstree" style="font-size:10px; width:200px;border-top:1px dotted #ccc;margin-top:15px;padding-top:5px;color:#666;" ></div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/jquery.jstree.js" ></script>
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree//lib/jquery.hotkeys.js" ></script>
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/lib/jquery.cookie.js" ></script>

<style media="all"  type="text/css">
a.removetag, a.removetag:hover {background:transparent url('/content/images/greyremove.jpg') no-repeat 0% 0%; width:12px; height:12px; display:inline-block; vertical-align:middle;}
a.removetag:hover {background:transparent url('/content/images/redremove.jpg') no-repeat 0% 0%;}

ul#albums li {margin-top:5px; color:#999;}
div#body{width:auto;}
</style>

<script language="javascript" type="text/javascript" >
    $(document).ready(function () {

        $("#albumstree").jstree({

            "json_data": {
                // I chose an ajax enabled tree - again - as this is most common, and maybe a bit more complex
                // All the options are the same as jQuery's except for `data` which CAN (not should) be a function
                "ajax": {
                    // the URL to fetch the data
                    "url": "/chuckconway/albums/GetNodes",
                    // this function is executed in the instance's scope (this refers to the tree instance)
                    // the parameter is the node being loaded (may be -1, 0, or undefined when loading the root nodes)
                    "data": function (n) {
                        // the result is fed to the AJAX request `data` option
                        return {
                            "operation": "get_children",
                            "id": n.attr ? n.attr("id").replace("node_", "") : 0
                        };
                    }
                }
            },


            "themes": {
                "theme": "default",
                "dots": true,
                "icons": false
            },

            "plugins": ["themes", "json_data", "ui", "crrm", "cookies", "dnd", "search", "types", "hotkeys", "contextmenu"]

        });

        $("div#albumstree a").live("click", function (e) {
            var self = $(this);
            var hiddenField = $('input#selectedalbums');
            var display = $('ul#albums');
            var id = self.parent().attr('id').replace("node_", "");
            var found = display.children('li#' + id);

            if (found[0] == null) {
                var text = self.text().replace(/\(\d*\)/g, "");
                var removeTag = $('<a href="#" title="remove \'untagged\'" class="removetag"></a>').click(remove);
                var albumTag = $('<li></li>').attr('id', id).text(text).append(removeTag);

                display.append(albumTag);

                var hiddenFieldValues = hiddenField.val() + id + ',';
                hiddenField.val(hiddenFieldValues);
            }

            return false;
        });

        loadSelectedAlbums();
    });

    function loadSelectedAlbums() {

        var items = $(parent.document).find('ul#albumsvalue li');
        var albumvalues = $('ul#albums');
        albumvalues.children().remove();

        items.each(function () {

            var id = $(this).attr('id');
            var text = $(this).text();

            var removeTag = $('<a href="#" title="remove \'untagged\'" class="removetag"></a>').click(remove);
            var albumTag = $('<li></li>').attr('id', id).text(text).append(removeTag);

            albumvalues.append(albumTag);
        });

        var selectedIds = $(parent.document).find('input#selectedalbums').val();
        $('input#selectedalbums').val(selectedIds);
    }

    function remove() {

        var self = $(this);
        var hiddenField = $('input#selectedalbums');
        var id = self.parent().attr('id');
        self.parent().remove();

        var values = hiddenField.val().replace(id + ',', '');
        hiddenField.val(values);
        return false;
    };

</script>
</asp:Content>
