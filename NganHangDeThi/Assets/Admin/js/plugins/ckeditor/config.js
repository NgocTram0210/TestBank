/// <reference path="../ckfinder/core/connector/aspx/connector.aspx" />
/// <reference path="../ckfinder/ckfinder.html" />
/// <reference path="../ckfinder/ckfinder.html" />
/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Assets/Admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Assets/Admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Assets/Admin/js/plugins/ckfinder/');
};
