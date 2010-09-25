<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Photos.EditPhotoView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>


<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css"> 
        @import url("/Content/jquery.autocomplete.css");   
    </style> 
        
        <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autocomplete.min.js"></script>
        <script type="text/javascript" src="/Scripts/jquery.ui.all.js"></script>
        <script type="text/javascript" src="/Scripts/jquery.jnotifica.min.js" ></script>
        <script type="text/javascript" language="javascript" >
            $(document).ready(function () {
                $('a.rotate').click(function () {
                    var anchor = $(this);
                    var linkText = anchor.text();
                    anchor.html('<img src=\"/Content/images/spinner.gif\" style=\"width:16px;height:16px;background-color:#fff; border:none;\" />');
                    $.get(this.href, function () {
                        var imageElement = $('#image');
                        var img_src = imageElement.attr('src');
                        var timestamp = new Date().getTime();
                        imageElement.load(function () { anchor.text(linkText); });
                        imageElement.attr('src', img_src + '?' + timestamp);
                    });
                    return false;
                });

                $("#Tags").autocomplete("/<%=Model.User.Username %>/photos/TagSearch", {
                    width: 320,
                    max: 10,
                    highlight: false,
                    multiple: true,
                    multipleSeparator: " ",
                    scroll: true,
                    scrollHeight: 300
                });

                var message = '<%= Model.UIMessage %>';
                showMessage(message);
            });

            function fadeIn(name) {

                var sidebar = $(name);

                sidebar.removeClass("hidden");
                sidebar.hide();
                sidebar.fadeIn("def");
            }		

			</script>	
            
    <style type="text/css"> 
        div#body {margin:0px;margin-top:40px; width:auto;}
        div.editphoto {margin-top:5px;width:450px;}       
    </style>		
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <% using (Html.BeginForm()){%>
        
        <div class="commentimage" >
            <%= string.Format("<img src=\"{0}\" id=\"image\" alt=\"{1}\" />", Model.ImagePath, Model.Story)%>  
            <p><a href="../rotateleft/<%= Model.Id %>" class="rotate link" title="rotate image 90% to the left">rotate left</a> | <a class="rotate link" href="../rotateright/<%= Model.Id %>" title="rotate image 90 degrees to the right">rotate right</a></p>  
        </div>
        
        <div class="form editphoto">
        <%= Html.ValidationSummary()%>
        <p style="margin-top:0px;"><label>Title</label><%= Html.TextBox("PhotoTitle", Model.PhotoTitle)%> <%= Html.ValidationMessage("UsernameErrorMessage")%></p>       

        
        <p><label>The Story</label><%= Html.TextArea("Story", Model.Story)%> <%= Html.ValidationMessage("PasswordErrorMessage")%></p>
        <p class="editdatesection"> 
            <span>
                <label>Year</label>
                <%= Html.TextBox("Year", Model.Year, new Dictionary<string, object>{{"class","editdate"},{"maxlength","4"}}) %>
            </span>
            
            <span style="margin-left:15px;" >
                <label>Month</label>
                <%= Html.DropDownList("SelectedMonth",Model.Month, new Dictionary<string, object>{{"class","editdate"}}) %>
            </span>

             <span style="margin-left:15px;">
                <label>Day</label>
                <%= Html.DropDownList("SelectedDay", Model.Day, new Dictionary<string, object>{{"class","editdate"}}) %>
            </span>
        </p>
        <div style="clear:both;" ></div>
        <p><label>Tags</label><%= Html.TextBox("Tags", Model.Tags)%> <%= Html.ValidationMessage("UsernameErrorMessage")%></p>
        <p><label>Privacy</label><%= Html.DropDownList("MediaStatus", Model.GetListItems(Model.Status))%> <%= Html.ValidationMessage("UsernameErrorMessage")%></p>       
    
        
        <%} %>
                  
      <%= Html.Message(Model.UIMessage) %>

      <br class="clearboth" />
      <p style="margin-top:20px;border-top:1px dotted #ddd;padding-top:10px;"><a class="buttonstyle" style="float:left;" href="#" title="edit the previous photo" >< Previous</a> <a class="buttonstyle" title="edit the next photo" style="float:right;" href="#" >Next ></a></p>

    </div>   
       
      
</asp:Content>
