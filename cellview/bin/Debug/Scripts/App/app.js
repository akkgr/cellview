var app = angular.module("demoapp", ["leaflet-directive"]);

app.controller('DemoController', function ($scope, $http, leafletData) {
    $scope.london = {
        lat: 38.0091,
        lng: 23.7758,
        zoom: 10
    };

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

        $http.get('/api/cells/cosmote/' + $scope.query).success(function (data) {
            angular.extend($scope, {
                markers: data,
            });
        }).        
        error(function (data, status, headers, config) {
            alert(data);
        });

        leafletData.getMarkers().then(function (markers) {            
            var s = markers.valueOf();
        });

    };

});