<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Upload.UserUploadView>" %>
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
       @import url("/Content/css/controls/jquery.treeview.css"); 
       @import url("/Content/css/controls/jquery.autocomplete.css");    
    </style> 

    <!-- google maps <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/jquery.googlemaps.js"></script>-->
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

    
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Maps/googleMaps.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/jquery.treeview.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/jquery.treeview.async.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Frameworks/swfobject.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.uploadify.v2.1.0.min.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autocomplete.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/common.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/upload/index.js"></script>

     <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
    
    <!-- jQuery and corMVC Application scripts. --> 
	<script type="text/javascript" src="/Scripts/Frameworks/Core/corMVC.js"></script>
    <script type="text/javascript" src="/Scripts/Pages/upload/index.js"></script>

    <!-- Controllers. --> 
	<script type="text/javascript" src="/Scripts/mvc/controllers/upload.js"></script> 
	
	<!-- Model. --> 
	<script type="text/javascript" src="/Scripts/mvc/models/upload/albumModel.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/upload/detailsModel.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/upload/locationModel.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/upload/uploadService.js"></script> 
	
	<!-- View. --> 
    <script type="text/javascript" src="/Scripts/mvc/views/upload/navigationView.js"></script> 
	<script type="text/javascript" src="/Scripts/mvc/views/upload/albumView.js"></script> 
	<script type="text/javascript" src="/Scripts/mvc/views/upload/detailsView.js"></script>
    <script type="text/javascript" src="/Scripts/mvc/views/upload/locationView.js"></script>  
    <script type="text/javascript" src="/Scripts/mvc/views/upload/pageNotFoundView.js"></script> 
    
    <script language="javascript" type="text/javascript" >
       var username = "<%= Model.User.Username %>";
       var map = null;
       var geocoder;

       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
       
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
             
            <div class="nav">
                 <ul class="tabs" style="float:left;">
                    <li><a href="#/" class="youarehere">Details</a></li>
                    <li><a href="#/location">Set Location</a></li>
                    <li><a href="#/albums">Add Albums</a></li>
                </ul>
                <br class="clearboth" />
            </div>
                    

            <div id="mvccontainer" ></div>
		</div>
 
        <div class="hyperlinks" style="margin-bottom:10px;border-top:1px dotted #666;padding-top:15px;text-align:right;" >            
            <a class="buttonstyle"  style="font-weight:bold;color:#fff;background-color:#419337;border:1px solid #419337;" href="javascript:$('#fileInput').uploadifySettings('script', '/MediaUpload/Photos/<%= Model.Id %>/<%= Model.BatchId  %>/?t=' + $('#tags').val()); $('#fileInput').uploadifyUpload();">Upload Files</a>  
            <a class="buttonstyle" style="font-weight:bold;color:#fff;background-color:#aaa;border:1px solid #aaa;" href="javascript:$('#fileInput').uploadifyClearQueue();">Clear Queue</a>    
            <a class="buttonstyle" style="margin-top: 15px; color:#fff; display:none; background-color:#777;" id="nextlink" href="/<%= Model.User.Username %>/upload/editphotodetails/<%= Model.BatchId  %>" >Next Step: Edit Photos' Details ></a>         
        </div>
        
        <input id="fileInput" name="fileInput" type="file" />            

    <script id="detailsTemplate" type="application/template">
    
                        <div class="form editphoto" style="padding-left:10px;">                                 
                        <form action="/chuckconway/upload/editphotodetails/" method="post"> 
                        <input id="photoId" type="hidden" name="photoId" />                                      
                            
                                 <p>
                                    <label>Location</label> <span id="locationvalue" >not set</span>
                                 </p>

                                 <p>
                                    <label>Albums</label> <span id="albumsvalue" >none</span>
                                </p>
                                <p class="editdatesection"> 
                                    <span>
                                        <label>Year</label>
                                        <input class="editdate" id="year" maxlength="4" name="Year" type="text" value="" />
                                    </span>
                                                        
                                    <span style="margin-left:15px;" >
                                        <label>Month</label>
                                        <select class="editdate" id="selectedmonth" name="SelectedMonth"></select>
                                    </span>

                                        <span style="margin-left:15px;">
                                        <label>Day</label>
                                        <select class="editdate" id="selectedday" name="SelectedDay"></select>
                                    </span>

                                    <br class="clearboth" />
                                </p>
                                <p>
                                    <label>Tags</label> <span>seperate tags by comma's or a space, combine multiple words into single-words (i.e. hot-topic) </span><input id="tags" name="Tags" type="text" value="" /> 
                                    <br /><strong>note:</strong> all photos without tags will be tagged as 'untagged'
                                </p>
                                <p><label>Privacy</label><select id="visibilityselect" name="MediaStatus"></select> </p>      
     
                    </form>
                    <br class="clearboth" />
                    <p style="margin-top:30px;color:maroon;"><strong>important:</strong> this page must remain open until all uploads are complete. </p>
                </div>   
    </script>

    <script id="navigationTemplate" type="application/template" >
    <div class="nav">
            <ul class="tabs" style="float:left;">
            <li><a href="#/" class="youarehere">Details</a></li>
            <li><a href="#/location">Set Location</a></li>
            <li><a href="#/albums">Add Albums</a></li>
        </ul>
        <br class="clearboth" />
    </div> 
    </script>

    <script id="locationTemplate" type="application/template" >
        
    <div id="left">
            
        <p>
            <label>Location Name</label>
            <input id="locationName" type="text" />
        </p>

        <p>Location name is the <em>friendly name</em> for the current location.</p>

        <p>For example, 95630 is Folsom, California's zipcode. It could be named: folsom, folsom-ca or folsom-california. </p>

    </div>


    <div id="right">
        <p>
            <input id="searchButton" type="submit" value="Search" /> 
            <input id="searchText" type="text" />                
        </p>
    </div>

    </script>

    <script id="albumTemplate" type="application/template">
         <div class="margintop40"> 
             <div id="albumstree" > 
                <ul id="albums" ></ul>     
             </div> 
 
             <div id="albumlist" >
                <ul id="selectedalbums" ></ul>
             </div> 
             <br class="clearboth;" /> 
        </div> 
    
    </script>
</asp:Content>
