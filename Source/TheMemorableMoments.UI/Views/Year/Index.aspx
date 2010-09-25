<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.YearIndexView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%= Model.Year %> - Year - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.Authorization) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader" style="margin-bottom:70px;">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
              
     <h2 class="grouptitle" ><%= Model.Year %></h2>
      
       <div>			
       <label class="count" > <%:Model.Media.Count %> photo(s)</label>

		<ul class="groupedphotos">						
			<% foreach (Media media in Model.Media){ %>
                <li>
                 <span class="image">
					<a class="showimage" name="<%=media.MediaId %>" rel="<%= Model.GetImageSrc(media, PhotoType.Websize) %>" href="<%= Model.UrlService.UserUrl("photos/show/" + Model.Set + "/#/photo/" + media.MediaId)  %>" title="<%= media.Title %>"> 
						<img src="<%= Model.GetImageSrc(media, PhotoType.Thumbnail)  %>" 
                        <%= (string.IsNullOrEmpty(media.Title) ? string.Empty : "title=\"" + media.Title + "\"") %> alt="Title #0" /> 
					</a> 
                  </span> 
				</li>  
                                               
            <%  } %>
       </ul>
       <br class="clearboth" />
    </div>

        <%if (!string.IsNullOrEmpty(Model.RelatedTags)) { %>	
  
        <h4>related</h4>
        <p class="hyperlinks" ><%= Model.RelatedTags %></p>

        <% } %>
    
    	     

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
    <script type="text/javascript" src="/Scripts/Pages/common.js"></script>
    <script type="text/javascript" src="/Scripts/Pages/shared/groupedphotos.js"></script>

    <script language="javascript" type="text/javascript"> 
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.Authorization.Owner.Username %>";
    </script>

	<style type="text/css">
         @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
         @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
         @import url("/Content/css/year/index.css");
         
         img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}         
    </style>
</asp:Content>
