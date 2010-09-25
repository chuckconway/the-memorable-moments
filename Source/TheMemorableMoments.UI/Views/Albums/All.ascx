<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TheMemorableMoments.UI.Models.Views.AlbumModels.BySearchingView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<% using (Html.BeginForm()){ %> 

        <label class="instructions" >adding photos</label>   
        <p style="margin-bottom:30px;" >Photos can be added to an album by browsing the images below and clicking the add link. Photos can also be found by using the tags or search views.  </p>


        <div class="viewarea whitebackground" >
        
        <p class="paging" > <%= Model.TotalCount + " " + (Model.TotalCount == 1 ? "photo" : "photos")  %> 
        <br /> <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
            <%= Html.Grid(Model.Pagination.PageSet).Attributes(new Dictionary<string, object>{{"class", "width100percent results"}})
            .Columns(c => c.For(x => Model.GetImageLink(Model.Set, x)).DoNotEncode().Named(string.Empty))
            .Columns(c => c.For(x => Model.RenderDetails(x)).DoNotEncode().Named(string.Empty))
            .Columns(c => c.For(x => string.Format("<a class=\"add\" href=\"/{0}/albums/AddMediaToAlbum/?mediaId={1}&albumId={2}\" >add</a>", 
            Model.Authorization.Owner.Username, x.MediaId, Model.Album.AlbumId)).DoNotEncode().Attributes(x => new Dictionary<string, object>{{"style","text-align:right;"}})) %>    

            <%= Model.Pagination.RenderedPagination  %>

        </div>
           <%} %> 
