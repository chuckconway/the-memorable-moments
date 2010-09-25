<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Recent.RecentIndex>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.Recent" %>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views.Recent" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Recent Uploads
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="userheader" >
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
		
        
    <div class="margintop50" >

       <% foreach (RecentIndex.PhotoAgeFriendlyName photoAgeFriendlyName in Model.GetPhotoAgeCollection()) {
       List<RecentUploads> uploads = Model.GetRecentUploadsByPhotoAge(photoAgeFriendlyName.PhotoAge);
       %>
       
       <% if (uploads.Count > 0){ %>

       <div class="groupedphotoscontainer" >
       <h2 class="grouptitle" ><%= photoAgeFriendlyName.Name %></h2>
       <label class="count"> <%:uploads.Count %> photo(s)</label>
		<ul class="groupedphotos">		
        <%  string key = Model.SetPersistentCollection(photoAgeFriendlyName.PhotoAge.ToString(), uploads); %>  				
			<% foreach (RecentUploads media in uploads){ %>
                <li>
                 <span class="image">
					<a name="<%=media.MediaId %>" class="showimage lightbox" rel="<%= Model.GetImageSrc(media, PhotoType.Websize)  %>" href="<%= Model.UrlService.UserUrl("photos/show/" + key + "/#/photo/" + media.MediaId)  %>" title="<%= media.Title %>"> 
						<img src="<%= Model.GetImageSrc(media, PhotoType.Thumbnail) %>" 
                        <%= (string.IsNullOrEmpty(media.Title) ? string.Empty : "title=\"" + media.Title + "\"") %> alt="Title #0" /> 
					</a> 
                  </span> 
				</li>  
                                             
            <%  } %>

            
       </ul>
        <br class="clearboth" />
        </div>
       <% } %>
      <% } %>
      
    </div>	



</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">

        <style type="text/css"> 
            @import url("/Content/css/recent/index.css");    
            img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px; }            
        </style> 
    
    <script language="javascript" type="text/javascript">
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.Authorization.Owner.Username %>";    
    </script>

    <script language="javascript" type="text/javascript" src="/Scripts/pages/recent/index.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="navigation" runat="server">
<%= Html.SiteNavigation(Model.Authorization) %>
</asp:Content>
