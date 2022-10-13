using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json;
using Serilog;
using SiPintar.Utilities;
using SiPintar.Views.Global.Other;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class MapConfigHelper
    {
        public static string DefaultLatitude { get; set; }
        public static string DefaultLongitude { get; set; }
        public static string GmapApiKey { get; set; }

        private static readonly int DefaultZoom = 14;
        private static readonly int MinZoom;
        private static readonly int MaxZoom = 24;


        public static void Initiate(GMapControl mainMap)
        {
            try
            {
                GMaps.Instance.Mode = AccessMode.ServerAndCache;
                GoogleMapProvider.Instance.ApiKey = GmapApiKey;
                var e = System.Net.Dns.GetHostEntry("www.google.com");
            }
            catch
            {
                mainMap.Manager.Mode = AccessMode.CacheOnly;
            }
        }

        public static void OnLoaded(GMapControl mainMap, bool isMouseCanWheel = true, int zoom = 0)
        {
            var latDefault = double.Parse(DefaultLatitude, CultureInfo.InvariantCulture);
            var lngDefault = double.Parse(DefaultLongitude, CultureInfo.InvariantCulture);

            mainMap.MapProvider = GMapProviders.GoogleMap;
            mainMap.Position = new PointLatLng(latDefault, lngDefault);
            mainMap.MinZoom = MinZoom;
            mainMap.MaxZoom = MaxZoom;
            mainMap.Zoom = zoom != 0 ? zoom : DefaultZoom;
            mainMap.ShowCenter = false;
            mainMap.CanDragMap = true;
            mainMap.DragButton = MouseButton.Left;
            mainMap.IgnoreMarkerOnMouseWheel = true;
            mainMap.MouseWheelZoomEnabled = isMouseCanWheel;
        }

        public static void SetMarker(GMapControl mainMap, List<MapMarkerObject> listMarker)
        {
            foreach (var item in listMarker)
            {
                if (!string.IsNullOrEmpty(item.Latitude) && !string.IsNullOrEmpty(item.Longitude))
                {
                    var isNumericLatitude = double.TryParse(item.Latitude, out _);
                    var isNumericLongitude = double.TryParse(item.Longitude, out _);

                    if (isNumericLatitude && isNumericLongitude)
                    {
                        // set current marker
                        var lat = double.Parse(item.Latitude, CultureInfo.InvariantCulture);
                        var lon = double.Parse(item.Longitude, CultureInfo.InvariantCulture);

                        var position = new PointLatLng(lat, lon);
                        mainMap.Position = position;
                        var currentMarker = new GMapMarker(position);

                        var markerTitle = item.MarkerInformation;
                        var markerColor = item.MarkerColor;

                        currentMarker.Shape = new MapMarkerView(currentMarker, markerTitle, markerColor);
                        currentMarker.Offset = new Point(-15, -15);
                        currentMarker.ZIndex = int.MaxValue;
                        mainMap.Markers.Add(currentMarker);
                    }
                }
            }
            mainMap.Zoom = 18;
        }

        public static void ClearMarker(GMapControl mainMap)
        {
            mainMap.Markers.Clear();
        }

        public static void SetCenter(GMapControl mainMap, string latitude, string longitude)
        {
            var lat = double.Parse(latitude, CultureInfo.InvariantCulture);
            var lon = double.Parse(longitude, CultureInfo.InvariantCulture);

            var position = new PointLatLng(lat, lon);
            mainMap.Position = position;
        }

        public static async Task<string[]> GetLatLonByAddressAsync(string address)
        {
            var geocodeUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=@address+View,+CA&sensor=false&key=@apiKey";

            string[] latLon = null;
            using var c = new HttpClient();
            try
            {
                geocodeUrl = geocodeUrl.Replace("@address", address).Replace("@apiKey", GmapApiKey);

                var result = await c.GetAsync(geocodeUrl);
                var content = await result.Content.ReadAsStringAsync();

                Debug.WriteLine(content);

                var final = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(content);

                if (final != null && final.results.Length > 0)
                {
                    latLon = new string[]
                    {
                        final.results[0].geometry.location.lat,
                        final.results[0].geometry.location.lng,
                    };
                }
            }
            catch (Exception exc)
            {
                Log.Logger.Error(exc.ToString());
            }

            return latLon;
        }

        public static async Task<string> GetAddressByLatLonAsync(string latitude, string longitude)
        {
            var geocodeUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng=@latitude,@longitude&key=@apiKey";

            string addressName = null;
            using var c = new HttpClient();
            try
            {
                geocodeUrl = geocodeUrl.Replace("@latitude", latitude.Replace(',', '.')).Replace("@longitude", longitude.Replace(',', '.')).Replace("@apiKey", GmapApiKey);

                var result = await c.GetAsync(geocodeUrl);
                var content = await result.Content.ReadAsStringAsync();

                Debug.WriteLine(geocodeUrl);
                Debug.WriteLine(content);

                var final = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(content);

                if (final != null && final.results.Length > 0)
                    addressName = final.results[0].formatted_address;
            }
            catch (Exception exc)
            {
                Log.Logger.Error(exc.ToString());
            }

            return addressName;
        }
    }

    [ExcludeFromCodeCoverage]
    public class MapMarkerObject
    {
        public int? IdPelanggan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MarkerInformation { get; set; }
        public string MarkerColor { get; set; } = "red";
    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class GoogleGeoCodeResponse
    {
        public string status { get; set; }
        public Results[] results { get; set; }

    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class Results
    {
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string[] types { get; set; }
        public address_component[] address_components { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class geometry
    {
        public string location_type { get; set; }
        public location location { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class address_component
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}
