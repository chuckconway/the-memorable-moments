<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Photos.EditPhotoView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>


<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        @import url("/Content/css/photos/edit.css"); 
        @import url("/Content/css/controls/jquery.autosearch.css");
        @import url("/Content/css/controls/jquery-ui-autocomplete.css");           
    </style> 

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>    
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Maps/googleMaps.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Frameworks/swfobject.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autosearch.min.js"></script>
   	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery-ui-autocomplete.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.form.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/photos/edit.js"></script>    

    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/jquery.jstree.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree//lib/jquery.hotkeys.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jsTree/lib/jquery.cookie.js" ></script>        
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.ui.all.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Mvc/Views/NavigationService.js"></script>

    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Core/corMVC.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Mvc/Controllers/edit.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Mvc/Models/Edit/EditModel.js" ></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Mvc/Views/Edit/EditView.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Mvc/Views/pageNotFoundView.js"></script>


    <script type="text/javascript" language="javascript" >
           var username = "<%= Model.Authorization.Owner.Username %>";
           var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
           var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;    
       
           var map = null;
           var geocoder;   
           var mapMarkers = new MapMarkers();	
           
    </script>	
            
    <style type="text/css"> 
        div#body {margin:0px;margin-top:40px; width:auto;}
        div.editphoto {margin-top:5px;width:450px;}       
    </style>		
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.HiddenFor(o => o.MediaKeys) %>
        
        
        
        <div class="commentimage" >
           <img src="/Content/images/blank.gif" style="display:none;" id="image" alt="" />
            <p id="rotatelinks" style="display:none;" class="whitebackground" ><a id="rotateLeft" href="#" class="rotate link" title="rotate image 90% to the left">rotate left</a> | <a id="rotateRight" class="rotate link" href="#" title="rotate image 90 degrees to the right">rotate right</a></p>  
            <p></p>
        </div>
        <form id="editphotoform" action="" method="post">
        <div class="form editphoto">
        <p style="margin-top:0px;"><label>Title</label><%= Html.TextBox("Title")%> </p>       
                
        <p><label>The Story</label><%= Html.TextArea("Story")%> <%= Html.ValidationMessage("PasswordErrorMessage")%></p>
        <p class="editdatesection"> 
            <span>
                <label>Year</label>
                <%= Html.TextBox("SelectedYear", "", new Dictionary<string, object>{{"class","editdate"},{"maxlength","4"}}) %>
            </span>
            
            <span style="margin-left:15px;" >
                <label>Month</label>
                <%= Html.DropDownList("SelectedMonth",Model.Month, new Dictionary<string, object>{{"class","editdate"}}) %>
            </span>

             <span style="margin-left:15px;">
                <label>Day</label>
                <%= Html.DropDownList("SelectedDay", Model.Day, new Dictionary<string, object>{{"class","editdate"}}) %>
            </span>
        </p>

        <div class="clearboth"></div>
            <div id="setlocationdropdown"> 
		        <label>Location (<a id="setlocation" class="hyperlinks setlocation dropdown" href="/<%= Model.Authorization.Owner.Username %>/upload/setlocation" >find</a>)</label> 
		        <div id="setlocationmenu" class="dropdown-menu"> 
			                <div>
                            <p>Search for a location by entering city, state or zip code. <strong>Click</strong> on the map to pin a new location.</p>
                            <p>
                                <label>Enter location</label>
                                <input id="searchText" type="text" style="width:300px;" /> <input style="width:auto;" id="searchButton" type="submit" value="Search" />       
                            </p>
                            </div>
                            <div id="map"></div>
		        </div> 
	        </div>
            
            <div style="margin-bottom:10px;" >
                <%= Html.TextBox("locationname") %>
                <span id="location" style="display:none;" >lat:<span id="lat" ></span>, long:<span id="long" ></span></span>
                <%= Html.Hidden("latitude", string.Empty) %>
                <%= Html.Hidden("longitude", string.Empty) %>
                <%= Html.Hidden("zoom", string.Empty) %>
                <%= Html.Hidden("maptypeid", string.Empty) %>
            </div>
    
            <p><%= Html.Hidden("selectedalbums") %></p>
                                    
            <div>
                <div id="albumsdropdown"> 
		            <label>Albums (<a class="hyperlinks selectalbum dropdown" id="dd2" href="/<%= Model.Authorization.Owner.Username %>/upload/selectalbums" >select</a>) : <span id="albums"></span></label> 
		            <div id="albumsmenu" class="dropdown-menu"> 
			            <div id="albumstree"></div>
		            </div> 
	            </div>
            </div> 

        <ul id="albumsvalue" ></ul>

        <div class="clearboth"></div>

        <p><label>Tags</label><%= Html.TextBox("Tags")%> </p>
        <p><label>Privacy</label><%= Html.DropDownList("MediaStatus", Model.GetListItems())%></p>   
        <p style="text-align:right;" class="submit"><input type="submit" style="width:auto;" value="Save" name="save" id="save" /></p>
                             
      <%= Html.Message(Model.UIMessage) %>

      <br class="clearboth" />
    <p id="photonavigation" class="hidden" style="text-align: center; font-size: 10px; margin-top:0px;color: #ccc;border-top:1px dotted #ccc;padding-top:3px;"><a class="hyperlinks whitebackground" id="prev" style="float:left;margin-top:0px;" href="#" title="view previous photo" >&lt; prev</a> � <span id="position" class="whitebackground" style="color:#666;" >1 of 25</span> � <a id="next" class="hyperlinks whitebackground" href="#" style="float:right;margin-top:0px;" title="view next photo" >next &gt;</a></p>

    </div>   
    </form> 
      
</asp:Content>
