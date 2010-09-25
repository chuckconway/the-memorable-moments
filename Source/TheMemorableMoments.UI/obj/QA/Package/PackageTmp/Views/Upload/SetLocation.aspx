<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div>
        <p>Search for you location by entering city, state or zip code. Once your location has been found it will automatically be saved. </p>
         <p>
            <label>Enter location</label>
            <input id="searchText" type="text" /> <input id="searchButton" type="submit" value="Search" />       
        </p>
     </div>
     <div id="map"></div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>    
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Maps/googleMaps.js"></script>
<script language="javascript" type="text/javascript">

    var map = null;
    var geocoder;

    $(document).ready(function () {

        var latitude = $(parent.document).find('input#latitude').val();
        var longitude = $(parent.document).find('input#longitude').val();

        $('input#latitude').val(latitude);
        $('input#longitude').val(longitude);

        var maps = new GoogleMaps();
        var divMap = $('div#map');
        maps.SetMarker(divMap[0], latitude, longitude);

        $('input#searchButton').click(function () {

            var address = $('input#searchText').val();
            if (geocoder) {
                geocoder.geocode({ 'address': address }, function (results, status) {

                    latitude = results[0].geometry.location.lat();
                    longitude = results[0].geometry.location.lng();

                    $('input#latitude').val(latitude);
                    $('input#longitude').val(longitude);

                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location
                        });
                    } else {
                        alert("Geocode was not successful for the following reason: " + status);
                    }
                });
            }
            
            return false;
        });



    });
</script>

<style media="all"  type="text/css">
    div#body{width:auto;}
    div#map {width:897px; height:500px;border:1px dotted #ccc;}
    input#searchText {width:500px;}
</style>

</asp:Content>
