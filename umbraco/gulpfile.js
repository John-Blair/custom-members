/// <binding />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    flatten = require('gulp-flatten'),
    iife = require('gulp-iife');

var exec = require('child_process').exec;

gulp.task("All-assembly-versions-update", function (cb) {
    exec('..\\version\\JB.VersionManager.exe', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})



var paths = {
    root: "C:\\Users\\John Blur\\Documents\\Visual Studio\\Projects\\u8\\johnblair\\"
};

paths.autocontentCP = paths.root + "JB.AutoContent\\create-package.bat";
gulp.task("DEPRECATED-JB.AutoContent++ver-ci-cd-package", function (cb) {
    exec('"' + paths.autocontentCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})


paths.errorpagesCP = paths.root + "JB.ErrorPages\\create-package.bat";
gulp.task("JB.ErrorPages++ver-ci-cd-package", function (cb) {
    exec('"' + paths.errorpagesCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})

paths.backofficeGlobalTradeCP = paths.root + "JB.Backoffice.GlobalTrade\\create-package.bat";
gulp.task("JB.Backoffice.GlobalTrade++ver-ci-cd-package", function (cb) {
    exec('"' + paths.backofficeGlobalTradeCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})


paths.jbSectionUtilitiesCP = paths.root + "JB.Section.Utilities\\create-package.bat";
gulp.task("JB.Section.Utilities++ver-ci-cd-package", function (cb) {
    exec('"' + paths.jbSectionUtilitiesCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})



paths.taxonomyCP = paths.root + "JB.Taxonomy\\create-package.bat";
gulp.task("JB.Taxonomy++ver-ci-cd-package", function (cb) {

    exec('"' + paths.taxonomyCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})



paths.removeMarketingDashboardsCP = paths.root + "JB.RemoveMarketingDashboards\\create-package.bat";
gulp.task("JB.RemoveMarketingDashboards++ver-ci-cd-package", function (cb) {

    exec('"' + paths.removeMarketingDashboardsCP + '"', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
})



gulp.task("Update-All-Nuspec-Versions-And-CI-CD-Nuget-Packages", gulp.series(["JB.ErrorPages++ver-ci-cd-package", "JB.Backoffice.GlobalTrade++ver-ci-cd-package", "JB.Taxonomy++ver-ci-cd-package", "JB.RemoveMarketingDashboards++ver-ci-cd-package", "JB.Section.Utilities++ver-ci-cd-package"]));
