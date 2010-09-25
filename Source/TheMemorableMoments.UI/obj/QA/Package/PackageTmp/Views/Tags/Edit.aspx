<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.EditTagModel>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

          <% using (Html.BeginForm()){%>
      <div class="form editphoto">
        <%= Html.ValidationSummary()%>
        <p><label>Tag Name</label><%= Html.TextBox("TagText", Model.TagText)%> </p>        
        <p><label>Description</label><%= Html.TextArea("TagDescription", Model.TagDescription)%> </p>
        <%: Html.Hidden("orginalTagText", Model.OrginalTagText) %>
        <p class="submit" style="margin-left:10px;" ><%=Html.SubmitButton("save", "Save")%></p>        
        <span class="message" ><%= Model.UIMessage %></span> 
      </div>
        <%} %>   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mastertitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">

    
    <style type="text/css"> 
        @import url("/Content/Generic.css");
        @import url("/Content/Site.css");
        @import url("/Content/jquery.fancybox-1.3.1.css");
    </style> 

     
    <script language="javascript" type="text/javascript" src="/Scripts/jquery.jnotifica.min.js"></script>

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
        div#header, div#footer{display:none;}
        div#body {margin:0; width:auto;}
        div.editphoto {margin-top:35px;width:450px;}       
    </style>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="navigation" runat="server">
</asp:Content>
