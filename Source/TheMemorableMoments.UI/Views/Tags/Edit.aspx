<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.EditTagModel>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <% using (Html.BeginForm()){%>
      <div class="form editphoto">
        <%= Html.ValidationSummary()%>
        <p><label>Tag Name</label><%= Html.TextBox("TagText", Model.TagText, new Dictionary<string, object>{{"style", "width:280px;"}})%> </p>        
        <p><label>Description</label><%= Html.TextArea("TagDescription", Model.TagDescription, new Dictionary<string, object>{{"style", "width:280px;height:100px;"}})%> </p>
        <%: Html.Hidden("orginalTagText", Model.OrginalTagText) %>
        <p class="submit" style="margin-left:10px;" ><%=Html.SubmitButton("save", "Save")%></p>        
        <%= Html.Message(Model.UIMessage) %>
      </div>
        <%} %>   

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">

    <style type="text/css"> 
        @import url("/Content/css/common.css");
    </style> 

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            var message = '<%= Model.UIMessage %>';
            showMessage(message);

            //Way Hacky
            //var message = $('span.albumtitle').val();
            if (message.length > 0) {
                $('input#Name').val('');
                $('textarea#Description').val('');
            }

        });
    
    </script>
    <style type="text/css"> 
        div#body {margin:0; width:auto;} 
    </style>

</asp:Content>
