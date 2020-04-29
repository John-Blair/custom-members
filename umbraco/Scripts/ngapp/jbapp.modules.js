// Bootstrap the jbutilities angular app
(function () {
    angular.module('jbapp', [
        'jbutilities.directives',
        'jbutilities.resources',
        'jbutilities.models',
        'jbutilities.filters',
        'jbsurvey.directives',
        'jbsurvey.services',
        'jbsurvey.resources',
        'jbsurvey.models',
        'jbsurvey.filters',
        'jbspa',
        'jbspa.directives',
        'jbspa.resources',
        'jbspa.models',
        'ngStorage',
        'gridster',
        'ui.bootstrap'
    ]).config(['$sessionStorageProvider', '$localStorageProvider',
        function ($sessionStorageProvider, $localStorageProvider) {
            $sessionStorageProvider.setKeyPrefix('jbapp-');
            $localStorageProvider.setKeyPrefix('jbapp-');
        }]);

    angular.module('jbutilities.models', []);
    angular.module('jbutilities.filters', []);
    angular.module('jbutilities.directives', []);
    angular.module('jbutilities.resources', ['jbutilities.models']);

    angular.module('jbsurvey.models', []);
    angular.module('jbsurvey.filters', []);
    angular.module('jbsurvey.services', ['jbsurvey.models']);
    angular.module('jbsurvey.resources', ['jbsurvey.models']);
    angular.module('jbsurvey.directives', ['jbsurvey.services']);

    angular.module('jbspa.models', []);
    angular.module('jbspa.directives', ['ngStorage',
        'gridster',
        'ui.bootstrap']);
    angular.module('jbspa.resources', ['jbspa.models']);
    angular.module('jbspa', [
        'jbspa.directives',
        'jbspa.resources',
        'jbspa.models']);

}());


