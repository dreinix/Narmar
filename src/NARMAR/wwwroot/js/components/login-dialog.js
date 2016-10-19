loader(function (ngModule) {
    ngModule.directive('narmarLoginDialog', () => ({
        templateUrl: "/Component/LoginDialog",
        restrict: 'E',
        scope: {
            visible: "=show"
        },
        controller: function ($scope,$rootScope,$http,$mdDialog,$mdToast) {
            /*** Data ***/
            $scope.$rootScope = $rootScope;
            $scope.loginData = {
                username: "Username",
                password: "Password"
            };

            /*** Functions ***/
            $scope.checkLogin = function () {
                return !!$scope.loginData.username && !!$scope.loginData.password;
            }
            $scope.doLogin = function () {
                // Post login data
                $http.post("/api/login", JSON.stringify($scope.loginData))
                .then(function (res) {
                    let data = res.data;
                    // check login state
                    if (data.status === "success") {
                        // save the session
                        localStorage["accessToken"] = data.token;
                        $rootScope.accessToken = localStorage["accessToken"];
                        // fetch user data
                        $rootScope.currentUser = $http.get(`/api/user/${data.uid}`).then(function (res) {
                            let data = res.data;
                            $mdDialog.hide()
                            $mdToast.show($mdToast.simple().textContent("Login Success. Welcome " + data.username));
                            $rootScope.currentUser = res.data;
                        }).catch(function (err) {
                            console.log("Error:", err);
                            $mdToast.show($mdToast.simple().textContent("Error: " + JSON.stringify(err)));
                        })
                    } else {
                        $mdToast.show($mdToast.simple().textContent("Login Failed. Try Again"));
                    }
                })
                .catch(function (err) {
                    console.log("Error:", err);
                    $mdToast.show($mdToast.simple().textContent("Error: " + JSON.stringify(err)));
                })
            }

            /*** Main ***/
            // recover last session
            if ($rootScope.accessToken !== undefined) {
                // get user id from token
                $http.get(`/api/login/${$rootScope.accessToken}`).then(function (res) {
                    console.log(res);
                    if (res.data.status === "success") {
                        // fetch user data
                        $rootScope.currentUser = $http.get(`/api/user/${res.data.uid}`).then(function (res) {
                            console.log(res);
                            $rootScope.currentUser = res.data;
                        }).catch(function (err) {
                            console.log("Error:", err);
                            $mdToast.show($mdToast.simple().textContent("Error: " + JSON.stringify(err)));
                        })
                    }
                }).catch(function (err) {
                    console.log("Error:", err);
                    $mdToast.show($mdToast.simple().textContent("Error: " + JSON.stringify(err)));
                })
            }
        }
    }));
});