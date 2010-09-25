<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Recent.RecentIndex>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model" %>
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
			<% foreach (RecentUploads media in uploads){ %>
                <li>
                 <span class="image">
					<a name="<%=media.MediaId %>" class="showimage lightbox" href="<%= Model.GetImageSrc(media, PhotoType.Websize)  %>" title="<%= media.Title %>"> 
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
            @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
            @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
            @import url("/Content/css/recent/index.css");    
            img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px; }            
        </style> 
    
    <script language="javascript" type="text/javascript">
       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.User.Username %>";    
    </script>

    <script language="javascript" type="text/javascript" src="/Scripts/pages/recent/index.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.lightbox-0.5.js"></script>  
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script> 

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="navigation" runat="server">
<%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>
</asp:Content>
