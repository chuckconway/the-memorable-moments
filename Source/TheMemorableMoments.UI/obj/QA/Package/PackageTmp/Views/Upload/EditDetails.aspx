<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Upload.EditPhotoDetailsView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.User.Username %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
    <ul class="uploadsteps" >
        <li><img src="/Content/Images/numbers/one.jpg" alt="Step One" /> Upload Photos</li>
        <li><img src="/Content/Images/numbers/two.jpg" alt="Step Two" /> <strong>Edit Photos' Details</strong></li>
    </ul>
    
    <p style="margin-top:60px;" >To edit a photo's details, click on any thumbnail. A pop-up window will allow you to change all details of the photo. </p>
    				<div class="navigation-container"> 
				    
					<div style="float:left;width:500px;"> 
			
						<ul class="groupedphotos"> 
						
						    <% foreach (Media media in Model.Media){ %>
                                 
                                 <li> 
                                    <span class="image" >
								        <a name="<%=media.MediaId %>" href="<%= PhotoHtmlHelper.GetImagePath(media, media.GetImageByPhotoType(PhotoType.Websize))  %>" title="<%= media.Title %>"> 
									         <img src="<%= PhotoHtmlHelper.GetImagePath(media, media.GetImageByPhotoType(PhotoType.Thumbnail))  %>" alt="Title #0" /> 
								        </a> 
                                    </span>
							    </li>    
                                    
                          <%  } %>   

					     </ul>
						</div>
						</div>
    

                                <div style="clear:both;" ></div>
                                <p style="text-align:right;font-size:13px;border-top:1px dotted #ccc;padding-top:10px;padding-right:10px;" ><a class="buttonstyle" href="/<%= Model.User.Username %>/upload" title="return to the homepage">Add more photos</a>  <a class="buttonstyle" href="/<%= Model.User.Username %>" title="return to the homepage">Finished</a></p>
                              <span class="message" id="message" ></span>  

    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
	<style type="text/css"> 
       @import url("/Content/css/controls/jquery.autocomplete.css");
       @import url("/Content/css/upload/editphotodetails.css");
    </style> 
	
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autocomplete.min.js"></script>
    <script type="text/javascript" src="/Scripts/Controls/jquery.form.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/upload/editphotodetails.js"></script>
      

    <script type="text/javascript">

    	var detailsPath = '/<%=Model.User.Username %>/upload/details/';
    	//var message = $('span#message');
    	var titleDetails = $('input#PhotoTitle');
    	var saveButton = $('input#save');
        var tagSearch = "/<%=Model.User.Username %>/photos/TagSearch";
			
	</script>
	
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="navigation" runat="server">
 <%= UserNavigationHelper.GetUserNavigation(Model.User.Username)%>
</asp:Content>
