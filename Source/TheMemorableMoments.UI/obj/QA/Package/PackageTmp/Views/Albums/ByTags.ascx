<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TheMemorableMoments.UI.Models.Views.AlbumModels.BySearchingView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<% using (Html.BeginForm()){ %> 
        <div class="form hidden whitebackground" style="margin-top: 20px;text-align:right;"><p style="padding-bottom:25px;" >
            
        <%= Html.TextBox("Text", Model.Text, new Dictionary<string, object> { { "style", "width:600px;" } })%> <%=Html.SubmitButton("Search", "Find by Tag")%> </p>
       
                    
        </div>
        <div class="viewarea hidden whitebackground" style="margin-left:10px;" >
        <p class="paging" > <%= Model.TotalCount + " " + (Model.TotalCount == 1 ? "photo" : "photos")%>
        <br /> <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
            <%= Html.Grid(Model.Pagination.PageSet).Attributes(new Dictionary<string, object>{{"class", "width100percent results"}}).Empty(string.Empty)
            .Columns(c => c.For(x => Model.GetImageLink(x)).DoNotEncode().Named(string.Empty))
            .Columns(c => c.For(x => Model.RenderDetails(x)).DoNotEncode().Named(string.Empty))
            .Columns(c => c.For(x => string.Format("<a class=\"add\" href=\"/{0}/albums/AddMediaToAlbum/?mediaId={1}&albumId={2}\" >add</a>", 
            Model.User.Username, x.MediaId, Model.Album.AlbumId)).DoNotEncode().Attributes(x => new Dictionary<string, object>{{"style", "text-align:right;"}})) %>    

            <%= Model.Pagination.RenderedPagination  %>

            <%= Html.Message(Model.UIMessage) %>
        </div>
           <%} %> 


