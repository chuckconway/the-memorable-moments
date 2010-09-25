<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tree
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <label>Albums</label>
    <span id="albums" ></span>
    <%= Html.Hidden("selectedalbums") %>

    <br />
    <br />
    <div id="albumstree" style="font-size:10px;" ></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mastertitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/jquery.jstree.js" ></script>
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree//lib/jquery.hotkeys.js" ></script>
<script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/lib/jquery.cookie.js" ></script>

<style media="all"  type="text/css">
a.removetag, a.removetag:hover {background:transparent url('/content/images/greyremove.jpg') no-repeat 0% 0%; width:12px; height:12px; display:inline-block; vertical-align:middle;}
a.removetag:hover {background:transparent url('/content/images/redremove.jpg') no-repeat 0% 0%;}

span#albums span {margin-right:10px; color:#999;}
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
            var display = $('span#albums');
            var id = self.parent().attr('id').replace("node_", "");

            var found = display.children('span#' + id);

            if (found[0] == null) {
                var text = self.text().replace(/\(\d*\)/g, "");
                var removeTag = $('<a href="#" title="remove \'untagged\'" class="removetag"></a>').click(remove);
                var span = $('<span></span>').attr('id', id).text(text).append(removeTag);
                display.append(span);

                var hiddenFieldValues = hiddenField.val() + id + ',';
                hiddenField.val(hiddenFieldValues);
            }

            return false;
        });

        function remove() {

            var self = $(this);
            var hiddenField = $('input#selectedalbums');
            var id = self.parent().attr('id');
            self.parent().remove();

            var values = hiddenField.val().replace(id + ',', '');
            hiddenField.val(values);
            return false;
        };
    });



</script>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="navigation" runat="server">
</asp:Content>
