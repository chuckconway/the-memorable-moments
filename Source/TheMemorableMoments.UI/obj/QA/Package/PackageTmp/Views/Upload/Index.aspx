<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Upload.UserUploadView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.User.Username %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
	<style type="text/css"> 
       @import url("/Content/css/upload/index.css");   
       @import url("/Content/css/controls/jquery.autosearch.css");    
       @import url("/Content/css/controls/jquery-ui-autocomplete.css");   
    </style> 

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>    
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Maps/googleMaps.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Frameworks/swfobject.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.uploadify.v2.1.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autosearch.min.js"></script>
   	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery-ui-autocomplete.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.form.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/common.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/upload/index.js"></script>    

    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/jquery.jstree.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree//lib/jquery.hotkeys.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/lib/jquery.cookie.js" ></script>
    
    <script language="javascript" type="text/javascript" >
       var username = "<%= Model.User.Username %>";
       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;    
       
       var map = null;
       var geocoder;   
       var mapMarkers = new MapMarkers();
    </script>    
   
    	
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.User.Username, Model.IsAuthenticated)%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
    <ul class="uploadsteps" >
        <li><img src="/Content/Images/numbers/one.jpg" alt="Step One" /> <strong>Upload Photos</strong></li>
        <li><img src="/Content/Images/numbers/two_grey.jpg" alt="Step Two" /> Edit Details</li>
    </ul>

    <div class="content">                           

               <div class="form editphoto" style="padding-left:10px;margin-top:50px;">       
                                         
                        <form id="uploadform" action="/<%= Model.User.Username %>/upload/savephotodetails" method="post"> 
                                
                                <p>All fields are optional. </p>
                                                             
                                    <div>
                                        <div id="setlocationdropdown"> 
		                                    <label>Location (<a id="setlocation" class="hyperlinks setlocation dropdown" href="/<%= Model.User.Username %>/upload/setlocation" >set</a>)</label> 
		                                    <div id="setlocationmenu" class="dropdown-menu"> 
			                                          <div>
                                                        <p>Search for you location by entering city, state or zip code.</p>
                                                        <p>
                                                            <label>Enter location</label>
                                                            <input id="searchText" type="text" style="width:400px;" /> <input id="searchButton" type="submit" value="Search" />       
                                                        </p>
                                                     </div>
                                                     <div id="map"></div>
		                                    </div> 
	                                    </div>
                                    </div> 

                                    <div style="margin-bottom:20px;" >
                                        <%= Html.TextBox("locationname") %>
                                        <span id="location" style="display:none;" >lat:<span id="lat" ></span>, long:<span id="long" ></span></span>
                                        <%= Html.Hidden("latitude", string.Empty) %>
                                        <%= Html.Hidden("longitude", string.Empty) %>
                                        <%= Html.Hidden("zoom", string.Empty) %>
                                        <%= Html.Hidden("maptypeid", string.Empty) %>
                                        <%= Html.Hidden("batchId", string.Empty) %>
                                    </div>
    

                                 <p style="margin-bottom:30px;" ><%= Html.Hidden("selectedalbums") %></p>
                                    
                                    <div>
                                        <div id="albumsdropdown"> 
		                                    <label>Albums (<a class="hyperlinks selectalbum dropdown" id="dd2" href="/<%= Model.User.Username %>/upload/selectalbums" >select</a>) : <span id="albums"></span></label> 
		                                    <div id="albumsmenu" class="dropdown-menu"> 
			                                   <div id="albumstree"></div>
		                                    </div> 
	                                    </div>
                                    </div> 

                                <ul id="albumsvalue" ></ul>

                                <div class="clearboth"></div>
                                <p class="editdatesection" style="margin-top:30px;"> 
                                    <span>
                                        <label>Year</label>
                                        <input class="editdate" id="selectedyear" maxlength="4" name="selectedyear" type="text" value="" />
                                    </span>
                                                        
                                    <span style="margin-left:15px;" >
                                        <label>Month</label>
                                        <%= Html.DropDownList("selectedMonth", Model.Month, new Dictionary<string, object>{{"class","editdate"}}) %> 
                                    </span>

                                    <span style="margin-left:15px;">
                                        <label>Day</label>
                                        <%= Html.DropDownList("selectedday", Model.Day, new Dictionary<string, object>{{"class","editdate"}}) %> 
                                    </span>

                                    <br class="clearboth" />
                                </p>
                                <p>
                                    <label>Tags</label> <span>seperate tags by comma's or a space, combine multiple words into single-words (i.e. hot-topic) </span><input id="tags" name="Tags" type="text" value="" /> 
                                    <br /><strong>note:</strong> all photos without tags will be tagged as 'untagged'
                                </p>
                                <p><label>Privacy</label><%= Html.DropDownList("MediaStatus", Model.GetListItems(MediaStatus.Public)) %></p>      
     
                    </form>
                    <br class="clearboth" />
                    <p style="margin-top:30px;color:maroon;"><strong>important:</strong> this page must remain open until all uploads are complete. </p>
                </div>   

		</div>
 
        <div class="uploadhyperlinks" style="" >            
            <a class="buttonstyle" id="uploadbutton" href="#">Upload Files</a>  
            <a class="buttonstyle" id="clearqueue" href="#">Clear Queue</a>    
            <a class="buttonstyle" id="nextlink" >Next Step: Edit Photos' Details ></a>         
        </div>
        
        <input id="fileInput" name="fileInput" type="file" />   
</asp:Content>