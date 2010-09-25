<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.TagsIndexView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  <%= Model.Name %> - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.Authorization) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader" >
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
     
     <div>       
     <h2 class="grouptitle" ><%: Model.Name %></h2>
              
     <% if(Model.Authorization.IsOwner && Model.Media != null) { %>
       <span class="hyperlinks" > [ <a class="edit" href="/<%= Model.Authorization.Owner.Username %>/tags/edit/<%= Model.Tag.TagText %>" >edit</a> ] </span>
     <%} %>

     <% if(Model.Media != null){ %>
     
     <% if(!string.IsNullOrEmpty(Model.Tag.Description)) { %>
     <p class="albumdescription"><%= Model.Tag.Description %></p>
      <%} %>
         <div>			
       <label class="count" > <%:Model.Media.Count %> photo(s)</label>

		<ul class="groupedphotos">						
			<% foreach (Media media in Model.Media){ %>
                <li>
                 <span class="image">
					<a class="showimage" name="<%=media.MediaId %>" rel="<%= Model.GetImageSrc(media, PhotoType.Websize) %>" href="<%= Model.UrlService.UserUrl("photos/show/" + Model.Set + "/#/photo/" + media.MediaId)  %>" title="<%= media.Title %>"> 
						<img src="<%= Model.GetImageSrc(media,PhotoType.Thumbnail)  %>" 
                        <%= (string.IsNullOrEmpty(media.Title) ? string.Empty : "title=\"" + media.Title + "\"") %> alt="Title #0" /> 
					</a> 
                  </span> 
				</li>  
                                               
            <%  } %>
       </ul>

       <br class="clearboth" />
    </div>	

   <%  } else { %>

   <p>I'm sorry, but we did not find any photos for '<%: Model.Tag.TagText %>'.</p>

    <%  } %>
    
    <%if (!string.IsNullOrEmpty(Model.RelatedTags)) { %>	
  
        <h4>related</h4>
        <p class="hyperlinks" ><%= Model.RelatedTags %></p>

        <% } %>
    </div> 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
    <script type="text/javascript" src="/Scripts/Pages/common.js"></script>
    <script type="text/javascript" src="/Scripts/Pages/shared/groupedphotos.js"></script>

    <script language="javascript" type="text/javascript"> 
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.Authorization.Owner.Username %>";

       $(document).ready(function () {

            $("a.edit").fancybox({
                'width': 300,
                'height':280,
                'autoScale': false,
                'transitionIn': 'fade',
                'transitionOut': 'fade',
                'speedIn': 400,
                'type': 'iframe',
                'overlayOpacity': 0.75,
                'overlayColor': '#000',
                onClosed : function(){ location.reload();}
            });

        });
    </script>

	<style type="text/css">
         @import url("/Content/css/tags/index.css");
         @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
         img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}         
    </style>
</asp:Content>
