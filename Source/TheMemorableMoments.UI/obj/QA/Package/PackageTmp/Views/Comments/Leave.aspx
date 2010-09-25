<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Comments.CommentsView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=(string.IsNullOrEmpty(Model.Media.Title) ? "Untitled" : Model.Media.Title) %> - leave comments  - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css"> 
        @import url("/Content/css/common.css");
        @import url("/Content/css/comments/leave.css");   
    </style> 

    <script language="javascript" type="text/javascript" src="/Scripts/Pages/comments/leave.js"></script>
    <script language="javascript" type="text/javascript" > var message = '<%= Model.UIMessage %>'; </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs) %>
    </div>
        
        <div class="commentimage hidden"  >
            <p ><%= (string.IsNullOrEmpty(Model.Media.Title) ? "Untitled" : Model.Media.Title)%></p>
                <%= string.Format("<img src=\"{0}\" style=\"margin-top:2px;\" id=\"image\" alt=\"{1}\" />", Model.GetImageSrc(Model.Media, PhotoType.Websize), HttpUtility.HtmlEncode(Model.Media.Description))%>           
         <%= Html.ValidationSummary() %>
         <% using (Html.BeginForm()) { %>
         
         <div class="comment hidden"  >
         
         <h3>Leave a Comment</h3>
         
            <p><%= Html.TextBox("Name", "Name (required)", new Dictionary<string, object>{{"class","commentinput"}})%><%= Html.ValidationMessage("NameErrorMessage")%></p>
            <p><%= Html.TextBox("Email", "Email (required, kept private)", new Dictionary<string, object> { { "class", "commentinput" } })%><%= Html.ValidationMessage("EmailErrorMessage")%></p>
            <p><%= Html.TextBox("Website", "Website (optional, [i.e. http://www.me.com])", new Dictionary<string, object> { { "class", "commentinput" } })%><%= Html.ValidationMessage("WebsiteErrorMessage")%></p>
            <p ><label>Comments <span>(required) </span></label> <textarea name="Comments" id="Comments" ></textarea><%= Html.ValidationMessage("CommentsErrorMessage")%></p>           

            <p class="submit"><%= Html.SubmitButton("CommentButton", "submit my comment", new Dictionary<string, object>{{"style","color:#fff;font-size:12px;"}}) %></p>
          </div>
         
         <% } %>       
        </div>
        
        <div class="postedcomment editphoto hidden">
        
        <%= string.Format("<h3><strong>{0}</strong> {1} for '{2}'</h3>", Model.Comments.Count, (Model.Comments.Count == 1 ? "Response" : "Responses"), (string.IsNullOrEmpty(Model.Media.Title) ? "Untitled" : Model.Media.Title))%>
         <% if (Model.Comments.Count > 0) 
            {
                for (int commentIndex = 0; commentIndex < Model.Comments.Count; commentIndex++)
                {%>
            
                <div class="commentsection<%= (commentIndex % 2 == 1 ? "" : " altcomment") %>">
                    <a name="comment-<%= Model.Comments[commentIndex].CommentId %>"></a> 
                    <p class="commentdate" ><%= Model.Comments[commentIndex].CommentDate.ToLongDateString()%></p>                                
                    <p class="commentauthor" > <%= Model.Comments[commentIndex].Name%> said:</p>                    
                <div class="commentcomment" ><%= Model.Comments[commentIndex].Text%></div> 
                                
                </div> 
         
         <%  }} else { %>
               <p class="nocommentsmessage" ><strong>No comments</strong> have been made, be the first to leave a comment!</p>         
          <% } %>
    <span class="message" ><%= Model.UIMessage %></span>
    </div>   
      
</asp:Content>
