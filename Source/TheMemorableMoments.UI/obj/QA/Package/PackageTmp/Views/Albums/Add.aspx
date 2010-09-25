<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.AddAlbumView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <% using (Html.BeginForm()){%>
      <div class="form editphoto">
        <%= Html.ValidationSummary()%>
        <p class="albumaddlinks" ><%= (string.IsNullOrEmpty(Model.AlbumCrumbs) ? string.Empty : "<strong>parent(s):</strong> " + Model.AlbumCrumbs )  %> </p>
        <p><label>Name</label><%= Html.TextBox("Name", Model.Name)%> <%= Html.ValidationMessage("UsernameErrorMessage")%></p>        
        <p><label>Description</label><%= Html.TextArea("Description", Model.Description)%> <%= Html.ValidationMessage("PasswordErrorMessage")%></p>
  
        <p class="submit" style="margin-left:10px;" ><%=Html.SubmitButton("save", "Save")%></p>        
        <span class="message" ><%= Model.UIMessage %></span> 
      </div>
        <%} %>    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            var message = '<%= Model.UIMessage %>';
            showMessage(message);
            
            //Way Hacky
            //var message = $('span.albumtitle').val();
            if(message.length > 0)
            {
                $('input#Name').val('');
                $('textarea#Description').val('');
            }

        });
    
    </script>
    <style type="text/css"> 
        div#body {margin:0; width:auto;}
        div.editphoto {margin-top:35px;width:450px;}       
    </style>

</asp:Content>

