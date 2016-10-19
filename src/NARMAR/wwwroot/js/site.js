loader(function (ngModule) {

    ngModule.filter("getEmail", function () {
        return function (user) {
            if (user === undefined)
                return null;
            return user.ContactMethods.find(function (val) { return val.Type === "email" }).Content;
        }
    });
});