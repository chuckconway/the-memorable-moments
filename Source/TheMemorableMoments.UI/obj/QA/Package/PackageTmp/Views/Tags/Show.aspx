<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.MediaGroupedByTagsView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 '<%= Model.Name %>' - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
   
    <div class="margintop40" >     
           <div class="alphabetcontainer" >
           
            <ul class="alphabet">
                <%= PagingHelper.RenderAlphabetPaging(Model.AlphabetPagingView, Model.User)%>
            </ul>
           
           </div>
           
          <div>
            <%string paging = PagingHelper.RenderNumericPaging(Model.NumericPaging, Model.User, Model.AlphabetPagingView.SelectedLetter); %>
        
        <% if (!string.IsNullOrEmpty(paging)){%>
        
        <div class="toppaginationcontainer">
            <ul class="pagination" >
                <%= paging%> 
            </ul> 
        </div>
        <% } %>
                
             <div class="photocontainer" >  
          
              <%
                  foreach (MediaGroupedByTag mediaByTag in Model.MediaByTags)
                  {
                      %>
                      
                     <h2 class="grouptitle"><%= mediaByTag.TagText %></h2>
                       <ul class="groupedphotos" >
                      <%
                      
                      foreach (Media file in mediaByTag.Media)
                      {
                          foreach (MediaFile mediaFile in file.MediaFiles)
                          {
                              if (mediaFile.MediaFormat == MediaFormat.Photo && mediaFile.PhotoType == PhotoType.Thumbnail)
                              {
                                  %><li>
                                        <span class="image">                                
                                           <a class="lightbox showimage" name="<%=file.MediaId %>" href="<%= Model.GetImageSrc(file, PhotoType.Websize)  %>" title="<%= file.Title %>" > <img id="<%=file.MediaId + "-" + Model.User.Username%>" title="<%=HttpUtility.HtmlEncode(file.Title)%>"  alt="<%=HttpUtility.HtmlEncode(file.Description)%>" src="<%=Model.GetImageSrc(file, PhotoType.Thumbnail)%>" /></a> 
                                        </span>
                                   </li><%
                                  break;
                              }
                          }
                      }
                      
                     %> 
                     </ul>
                     <div class="clearboth"></div>
                      <%
                  }
              %> 
              
              </div> 
              <div class="clearboth"></div>
           
        <% if (!string.IsNullOrEmpty(paging)){%>
        
            <div class="bottompaginationcontainer">
                <ul class="pagination" >
                    <%= paging%> 
                </ul> 
            </div>
        <% } %>
          
          </div>    
</div>  

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
 
    <script language="javascript" type="text/javascript" src="/Scripts/pages/user/index.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
    <script language="javascript" type="text/javascript"  src="/Scripts/Controls/jquery.jnotifica.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.lightbox-0.5.js"></script>  
    <script language="javascript" type="text/javascript" src="/Scripts/controls/FancyBox/jquery.fancybox-1.3.1.js"></script> 
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script>   

	<style type="text/css">
         @import url("/Content/css/tags/show.css");
         @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
    </style>

     <style media="all" type="text/css">
        img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px;}
    </style>


    <script language="javascript" type="text/javascript">
     var username = "<%= Model.User.Username %>";
       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
        $(document).ready(function () {

            $('ul li span a.lightbox').lightBox({
                overlayBgColor: '#000',
                overlayOpacity: 0.8,
                imageLoading: '/content/images/lightbox/lightbox-ico-loading.gif',
                imageBtnClose: '/content/images/lightbox/lightbox-btn-close.gif',
                imageBtnPrev: '/content/images/lightbox/lightbox-btn-prev.gif',
                imageBtnNext: '/content/images/lightbox/lightbox-btn-next.gif',
                containerResizeSpeed: 350
            });

        });
    
    </script>
</asp:Content>

