<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.MediaGroupedByTagsView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 '<%= Model.Name %>' - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.Authorization) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
   
    <div class="margintop40" >     
           <div class="alphabetcontainer" >
           
            <ul class="alphabet">
                <%= PagingHelper.RenderAlphabetPaging(Model.AlphabetPagingView, Model.Authorization.Owner)%>
            </ul>
           
           </div>
           
          <div>
            <%string paging = PagingHelper.RenderNumericPaging(Model.NumericPaging, Model.Authorization.Owner, Model.AlphabetPagingView.SelectedLetter); %>
        
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
                      string key = Model.SetPersistentCollection("alphabet_tags_" + Model.Authorization.Owner.Id + "_" + mediaByTag.TagText, mediaByTag.Media);
                      foreach (Media file in mediaByTag.Media)
                      {
                            %><li>
                                <span class="image">                                
                                    <a class="lightbox showimage" name="<%=file.MediaId %>" href="/<%= Model.Authorization.Owner.Username %>/photos/show/<%= key %>/#/photo/<%= file.MediaId %>" rel="<%= Model.GetImageSrc(file, PhotoType.Websize)  %>" title="<%= file.Title %>" > <img id="<%=file.MediaId + "-" + Model.Authorization.Owner.Username%>" title="<%=HttpUtility.HtmlEncode(file.Title)%>"  alt="<%=HttpUtility.HtmlEncode(file.Description)%>" src="<%=Model.GetImageSrc(file, PhotoType.Thumbnail)%>" /></a> 
                                </span>
                            </li><%
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
 
    <script language="javascript" type="text/javascript" src="/Scripts/pages/tags/show.js"></script>

	<style type="text/css">
         @import url("/Content/css/tags/show.css");
    </style>

     <style media="all" type="text/css">
        img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}
    </style>


    <script language="javascript" type="text/javascript">
     var username = "<%= Model.Authorization.Owner.Username %>";
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
    
    </script>
</asp:Content>

