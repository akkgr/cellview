var app = angular.module("demoapp", []);

app.controller('DemoController', function ($scope, $http) {
    //$scope.athens = {
    //    lat: 38.0091,
    //    lng: 23.7758,
    //    zoom: 10
    //};

    var map = L.map('map').setView([38.0091, 23.7758], 10);

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '',
        maxZoom: 18
    }).addTo(map);

    var markers = new L.MarkerClusterGroup();
    map.addLayer(markers);

    $scope.markers = markers;
    $scope.map = map;

    //$scope.markers = {
    //    main_marker: {
    //        lat: 38.0091,
    //        lng: 23.7758,
    //        focus: true,
    //        message: "Athens, Greece",
    //        title: "Athens",
    //        draggable: false
    //    }
    //};

    //$scope.icon = {
    //    type: 'awesomeMarker',
    //    icon: 'arrow-up',
    //    markerColor: 'red'
    //};

    $scope.finditem = function () {

        //var redMarker = L.AwesomeMarkers.icon({
        //    icon: 'arrow-up',
        //    markerColor: 'red'
        //});

        $http.get('/api/cells/cosmote/' + $scope.query).success(function (data) {
            angular.extend($scope, {
                cells: data,
            });
            data.forEach(function (item) {
                var marker = L.marker([item.lat, item.lng]);//, { icon: redMarker });
                marker.setIconAngle(marker.iconAngle);
                marker.on('click', function () {
                    L.circle([item.lat, item.lng], item.range).setDirection(item.iconAngle, item.radius).addTo(map);
                });
                $scope.markers.addLayer(marker);
            });
        }).        
        error(function (data, status, headers, config) {
            alert(data);
        });

    };

});