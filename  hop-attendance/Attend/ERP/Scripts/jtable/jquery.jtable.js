﻿/* 

jTable 1.5.1
http://www.jtable.org

---------------------------------------------------------------------------

Copyright (C) 2011-2012 by Halil İbrahim Kalkan (http://www.halilibrahimkalkan.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

/************************************************************************
* CORE jTable module                                                    *
*************************************************************************/
(function ($) {

    $.widget("hik.jtable", {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {

            //Options
            actions: {},
            fields: {},
            animationsEnabled: true,
            defaultDateFormat: 'yy-mm-dd',
            dialogShowEffect: 'fade',
            dialogHideEffect: 'fade',
            showCloseButton: false,

            //Events
            closeRequested: function (event, data) { },
            formCreated: function (event, data) { },
            formSubmitting: function (event, data) { },
            formClosed: function (event, data) { },
            loadingRecords: function (event, data) { },
            recordsLoaded: function (event, data) { },
            rowInserted: function (event, data) { },
            rowsRemoved: function (event, data) { },

            //Localization
            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: 'Loading records...',
                noDataAvailable: 'No data available!',
                areYouSure: 'Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}'
            }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _$mainContainer: null, //Reference to the main container of all elements that are created by this plug-in (jQuery object)

        _$table: null, //Reference to the main <table> (jQuery object)
        _$tableBody: null, //Reference to <body> in the table (jQuery object)
        _$tableRows: null, //Array of all <tr> in the table (except "no data" row) (jQuery object array)

        _$bottomPanel: null, //Reference to the panel at the bottom of the table (jQuery object)

        _$busyDiv: null, //Reference to the div that is used to block UI while busy (jQuery object)
        _$busyMessageDiv: null, //Reference to the div that is used to show some message when UI is blocked (jQuery object)
        _$errorDialogDiv: null, //Reference to the error dialog div (jQuery object)

        _columnList: null, //Name of all data columns in the table (select column and command columns are not included) (string array)
        _fieldList: null, //Name of all fields of a record (defined in fields option) (string array)
        _keyField: null, //Name of the key field of a record (that is defined as 'key: true' in the fields option) (string)

        _firstDataColumnOffset: 0, //Start index of first record field in table columns (some columns can be placed before first data column, such as select checkbox column) (integer)
        _lastPostData: null, //Last posted data on load method (object)

        _cache: null, //General purpose cache dictionary (object)

        /************************************************************************
        * CONSTRUCTOR AND INITIALIZATION METHODS                                *
        *************************************************************************/

        /* Contructor.
        *************************************************************************/
        _create: function () {

            //Initialization
            this._normalizeFieldsOptions();
            this._initializeFields();
            this._createFieldAndColumnList();

            //Creating DOM elements
            this._createMainContainer();
            this._createTableTitle();
            this._createTable();
            this._createBottomPanel();
            this._createBusyPanel();
            this._createErrorDialogDiv();
            this._addNoDataRow();
        },

        /* Normalizes some options for all fields (sets default values).
        *************************************************************************/
        _normalizeFieldsOptions: function () {
            var self = this;
            $.each(self.options.fields, function (fieldName, props) {
                self._normalizeFieldOptions(fieldName, props);
            });
        },

        /* Normalizes some options for a field (sets default values).
        *************************************************************************/
        _normalizeFieldOptions: function (fieldName, props) {
            props.listClass = props.listClass || '';
            props.inputClass = props.inputClass || '';
        },

        /* Intializes some private variables.
        *************************************************************************/
        _initializeFields: function () {
            this._lastPostData = {};
            this._$tableRows = [];
            this._columnList = [];
            this._fieldList = [];
            this._cache = [];
        },

        /* Fills _fieldList, _columnList arrays and sets _keyField variable.
        *************************************************************************/
        _createFieldAndColumnList: function () {
            var self = this;

            $.each(self.options.fields, function (name, props) {

                //Add field to the field list
                self._fieldList.push(name);

                //Check if this field is the key field
                if (props.key == true) {
                    self._keyField = name;
                }

                //Add field to column list if it is shown in the table
                if (props.list != false && props.type != 'hidden') {
                    self._columnList.push(name);
                }
            });
        },

        /* Creates the main container div.
        *************************************************************************/
        _createMainContainer: function () {
            this._$mainContainer = $('<div class="jtable-main-container"></div>').appendTo(this.element);
        },

        /* Creates title of the table if a title supplied in options.
        *************************************************************************/
        _createTableTitle: function () {
            var self = this;
            if (!self.options.title) {
                return;
            }

            //TODO: May use caption element instead of a seperated div element?
            var $titleDiv = $('<div class="jtable-title"><div class="jtable-title-text">' + self.options.title + '</div></div>').appendTo(self._$mainContainer);
            if (self.options.showCloseButton) {
                $('<button class="jtable-command-button jtable-close-button" title="' + self.options.messages.close + '"><span>' + self.options.messages.close + '</span></button>')
                    .appendTo($titleDiv)
                    .click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        self._onCloseRequested();
                    });
            }
        },

        /* Creates table.
        *************************************************************************/
        _createTable: function () {
            this._$table = $('<table class="jtable"></table>').appendTo(this._$mainContainer);
            this._createTableHead();
            this._createTableBody();
        },

        /* Creates header (all column headers) of the table.
        *************************************************************************/
        _createTableHead: function () {
            var $thead = $('<thead></thead>').appendTo(this._$table);
            this._addRowToTableHead($thead);
        },

        /* Adds tr element to given thead element
        *************************************************************************/
        _addRowToTableHead: function ($thead) {
            var $tr = $('<tr></tr>').appendTo($thead);
            this._addColumnsToHeaderRow($tr);
        },

        /* Adds column header cells to given tr element.
        *************************************************************************/
        _addColumnsToHeaderRow: function ($tr) {
            for (var i = 0; i < this._columnList.length; i++) {
                var fieldName = this._columnList[i];
                var $headerCell = this._createHeaderCellForField(fieldName, this.options.fields[fieldName]);
                $headerCell.data('fieldName', fieldName).appendTo($tr);
            }
        },

        /* Creates a header cell for given field.
        *  Returns th jQuery object.
        *************************************************************************/
        _createHeaderCellForField: function (fieldName, field) {
            return $('<th class="jtable-column-header"' + (field.width != undefined ? ' style="width:' + (field.width || '10%') : '') +
                '"><div class="jtable-column-header-container"><span class="jtable-column-header-text">' + field.title +
                '</span></div></th>')
                .data('fieldName', fieldName);
        },

        /* Creates an empty header cell that can be used as command column headers.
        *************************************************************************/
        _createEmptyCommandHeader: function () {
            return $('<th class="jtable-command-column-header"></th>');
        },

        /* Creates tbody tag and adds to the table.
        *************************************************************************/
        _createTableBody: function () {
            this._$tableBody = $('<tbody></tbody>').appendTo(this._$table);
        },

        /* Creates bottom panel and adds to the page.
        *************************************************************************/
        _createBottomPanel: function () {
            this._$bottomPanel = $('<div class="jtable-bottom-panel"></div>').appendTo(this._$mainContainer);
        },

        /* Creates a div to block UI while jTable is busy.
        *************************************************************************/
        _createBusyPanel: function () {
            this._$busyMessageDiv = $('<div class="jtable-busy-message"></div>').prependTo(this._$mainContainer);
            this._$busyDiv = $('<div class="jtable-busy-panel-background"></div>').prependTo(this._$mainContainer);
            this._hideBusy();
        },

        /* Creates and prepares error dialog div.
        *************************************************************************/
        _createErrorDialogDiv: function () {
            var self = this;

            self._$errorDialogDiv = $('<div></div>').appendTo(self._$mainContainer);
            self._$errorDialogDiv.dialog({
                autoOpen: false,
                show: self.options.dialogShowEffect,
                hide: self.options.dialogHideEffect,
                modal: true,
                title: self.options.messages.error,
                buttons: [{
                    text: self.options.messages.close,
                    click: function () {
                        self._$errorDialogDiv.dialog('close');
                    }
                }]
            });
        },

        /************************************************************************
        * PUBLIC METHODS                                                        *
        *************************************************************************/

        /* Loads data using AJAX call, clears table and fills with new data.
        *************************************************************************/
        load: function (postData, completeCallback) {
            this._lastPostData = postData;
            this._reloadTable(completeCallback);
        },

        /* Refreshes (re-loads) table data with last postData.
        *************************************************************************/
        reload: function (completeCallback) {
            this._reloadTable(completeCallback);
        },

        /* Completely removes the table from it's container.
        *************************************************************************/
        destroy: function () {
            this.element.empty();
            $.Widget.prototype.destroy.call(this);
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* LOADING RECORDS  *****************************************************/

        /* Performs an AJAX call to specified URL.
        *************************************************************************/
        _reloadTable: function (completeCallback) {
            var self = this;

            //Disable table since it's busy
            self._showBusy(self.options.messages.loadingMessage);

            //Generate URL (with query string parameters) to load records
            var loadUrl = self._createRecordLoadUrl();

            //Load data from server
            self._onLoadingRecords();
            self._performAjaxCall(
                loadUrl,
                self._lastPostData,
                true, //asynchronous
                function (data) { //success
                    self._hideBusy();

                    //Show the error message if server returns error
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        return;
                    }

                    //Re-generate table rows
                    self._removeAllRows('reloading');
                    self._addRecordsToTable(data.Records);

                    self._onRecordsLoaded(data);

                    //Call complete callback
                    if (completeCallback) {
                        completeCallback();
                    }
                },
                function () {
                    self._hideBusy();
                    self._showError(self.options.messages.serverCommunicationError);
                });
        },

        /* Creates URL to load records.
        *************************************************************************/
        _createRecordLoadUrl: function () {
            return this.options.actions.listAction;
        },

        /* TABLE MANIPULATION METHODS *******************************************/

        /* Creates a row from given record
        *************************************************************************/
        _createRowFromRecord: function (record) {
            var $newTableRow = $('<tr></tr>').data('record', record);
            this._addCellsToRowUsingRecord($newTableRow);
            return $newTableRow;
        },

        /* Adds all cells to given row.
        *************************************************************************/
        _addCellsToRowUsingRecord: function ($row) {
            var record = $row.data('record');
            for (var i = 0; i < this._columnList.length; i++) {
                var columnName = this._columnList[i];
                var $column = $('<td class="' + this.options.fields[columnName].listClass + '"></td>').appendTo($row);
                $column.append((this._getDisplayTextForRecordField(record, this._columnList[i]) || ''));
            }
        },

        /* Adds a list of records to the table.
        *************************************************************************/
        _addRecordsToTable: function (records) {
            var self = this;

            $.each(records, function (index, record) {
                self._addRowToTable(self._createRowFromRecord(record));
            });

            self._refreshRowStyles();
        },

        /* Adds a single row to a specific index of the table.
        *  If no index specified, adds to end of the table.
        *************************************************************************/
        _addRowToTable: function ($tableRow, index, isNewRow, animationsEnabled) {
            var self = this;

            //set default value
            if (isNewRow != true) {
                isNewRow = false;
            }

            if (animationsEnabled != false) {
                animationsEnabled = true;
            }

            //Remove 'no data' row if this is first row
            if (this._$tableRows.length <= 0) {
                this._removeNoDataRow();
            }

            //Add new row to the table according to it's index
            index = this._normalizeNumber(index, 0, this._$tableRows.length, this._$tableRows.length);
            if (index == this._$tableRows.length) {
                //add as last row
                this._$tableBody.append($tableRow);
                this._$tableRows.push($tableRow);
            } else if (index == 0) {
                //add as first row
                this._$tableBody.prepend($tableRow);
                this._$tableRows.unshift($tableRow);
            } else {
                //insert to specified index
                this._$tableRows[index - 1].after($tableRow);
                this._$tableRows.splice(index, 0, $tableRow);
            }

            this._onRowInserted($tableRow, isNewRow);

            //Show animation if needed
            if (isNewRow == true) {
                self._refreshRowStyles();
                if (self.options.animationsEnabled && animationsEnabled) {
                    self._showNewRowAnimation($tableRow);
                }
            }
        },

        /* Shows created animation for a table row
        *************************************************************************/
        _showNewRowAnimation: function ($tableRow) {
            $tableRow.addClass('jtable-row-created', 'slow', '', function () {
                $tableRow.removeClass('jtable-row-created', 5000);
            });
        },

        /* Removes a row or rows (jQuery selection) from table.
        *************************************************************************/
        _removeRowsFromTable: function ($rows, reason) {
            var self = this;

            //Check if any row specified
            if ($rows.length <= 0) {
                return;
            }

            //remove from DOM
            $rows.remove();

            //remove from _$tableRows array
            $rows.each(function () {
                self._$tableRows.splice(self._findRowIndex($(this)), 1);
            });

            self._onRowsRemoved($rows, reason);

            //Add 'no data' row if all rows removed from table
            if (self._$tableRows.length == 0) {
                self._addNoDataRow();
            }

            self._refreshRowStyles();
        },

        /* Finds index of a row in table.
        *************************************************************************/
        _findRowIndex: function ($row) {
            return this._findIndexInArray($row, this._$tableRows, function ($row1, $row2) {
                return $row1.data('record') == $row2.data('record');
            });
        },

        /* Removes all rows in the table and adds 'no data' row.
        *************************************************************************/
        _removeAllRows: function (reason) {
            //If no rows does exists, do nothing
            if (this._$tableRows.length <= 0) {
                return;
            }

            //Select all rows (to pass it on raising _onRowsRemoved event)
            var $rows = this._$tableBody.find('tr');

            //Remove all rows from DOM and the _$tableRows array
            this._$tableBody.empty();
            this._$tableRows = [];

            this._onRowsRemoved($rows, reason);

            //Add 'no data' row since we removed all rows
            this._addNoDataRow();
        },

        /* Adds "no data available" row to the table.
        *************************************************************************/
        _addNoDataRow: function () {
            var totalColumnCount = this._$table.find('thead th').length;
            $('<tr class="jtable-no-data-row"></tr>')
                .append('<td colspan="' + totalColumnCount + '">' + this.options.messages.noDataAvailable + '</td>')
                .appendTo(this._$tableBody);
        },

        /* Removes "no data available" row from the table.
        *************************************************************************/
        _removeNoDataRow: function () {
            this._$tableBody.find('.jtable-no-data-row').remove();
        },

        /* Refreshes styles of all rows in the table
        *************************************************************************/
        _refreshRowStyles: function () {
            for (var i = 0; i < this._$tableRows.length; i++) {
                if (i % 2 == 0) {
                    this._$tableRows[i].addClass('jtable-row-even');
                } else {
                    this._$tableRows[i].removeClass('jtable-row-even');
                }
            }
        },

        /* RENDERING FIELD VALUES ***********************************************/

        /* Gets text for a field of a record according to it's type.
        *************************************************************************/
        _getDisplayTextForRecordField: function (record, fieldName) {
            var field = this.options.fields[fieldName];
            var fieldValue = record[fieldName];

            if (field.display) {
                return field.display({ record: record });
            }

            if (field.type == 'date') {
                return this._getDisplayTextForDateRecordField(field, fieldValue);
            } else if (field.type == 'checkbox') {
                return this._getCheckBoxTextForFieldByValue(fieldName, fieldValue);
            } else if (field.options) {
                var x = this._getOptionsWithCaching(fieldName)[fieldValue];
                return x;
            } else {
                return fieldValue;
            }
        },

        /* Gets text for a date field.
        *************************************************************************/
        _getDisplayTextForDateRecordField: function (field, fieldValue) {
            if (!fieldValue) {
                return '';
            }

            var displayFormat = field.displayFormat || this.options.defaultDateFormat;
            var date = this._parseDate(fieldValue);
            return $.datepicker.formatDate(displayFormat, date);
        },

        /* Parses given date string to a javascript Date object.
        *  Given string must be formatted one of the samples shown below:
        *  /Date(1320259705710)/
        *  2011-01-01 20:32:42 (YYYY-MM-DD HH:MM:SS)
        *  2011-01-01 (YYYY-MM-DD)
        *************************************************************************/
        _parseDate: function (dateString) {
            if (dateString.indexOf('Date') >= 0) { //Format: /Date(1320259705710)/
                return new Date(parseInt(dateString.substr(6)));
            } else if (dateString.length == 10) { //Format: 2011-01-01
                return new Date(parseInt(dateString.substr(0, 4)), parseInt(dateString.substr(5, 2)) - 1, parseInt(dateString.substr(8, 2)));
            } else if (dateString.length == 19) { //Format: 2011-01-01 20:32:42
                return new Date(parseInt(dateString.substr(0, 4)), parseInt(dateString.substr(5, 2)) - 1, parseInt(dateString.substr(8, 2)), parseInt(dateString.substr(11, 2)), parseInt(dateString.substr(14, 2)), parseInt(dateString.substr(17, 2)));
            } else {
                this._logWarn('Given date is no properly formatted: ' + dateString);
                return new Date(); //Default value!
            }
        },

        /* ERROR DIALOG *********************************************************/

        /* Shows error message dialog with given message.
        *************************************************************************/
        _showError: function (message) {
            this._$errorDialogDiv.html(message).dialog('open');
        },

        /* BUSY PANEL ***********************************************************/

        /* Shows busy indicator and blocks table UI.
        *************************************************************************/
        _showBusy: function (message) {
            if (!this._$busyMessageDiv.is(':visible')) {
                this._$busyDiv.width(this._$mainContainer.width());
                this._$busyDiv.height(this._$mainContainer.height());
                this._$busyDiv.show();
                this._$busyMessageDiv.show();
            }

            this._$busyMessageDiv.html(message);
        },

        /* Hides busy indicator and unblocks table UI.
        *************************************************************************/
        _hideBusy: function () {
            this._$busyDiv.hide();
            this._$busyMessageDiv.html('').hide();
        },

        /* Returns true if jTable is busy.
        *************************************************************************/
        _isBusy: function () {
            return this._$busyMessageDiv.is(':visible');
        },

        /* COMMON METHODS *******************************************************/

        /* Performs an AJAX call to specified URL.
        *************************************************************************/
        _performAjaxCall: function (url, postData, async, success, error) {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: postData,
                async: async,
                success: function (data) {
                    success(data);
                },
                error: function () {
                    error();
                }
            });
        },

        /* Gets a jQuery row object according to given record key
        *************************************************************************/
        getRowByKey: function (key) {
            for (var i = 0; i < this._$tableRows.length; i++) {
                if (key == this._$tableRows[i].data('record')[this._keyField]) {
                    return this._$tableRows[i];
                }
            }

            return null;
        },

        /************************************************************************
        * EVENT RAISING METHODS                                                 *
        *************************************************************************/

        _onLoadingRecords: function () {
            this._trigger("loadingRecords", null, {});
        },

        _onRecordsLoaded: function (data) {
            this._trigger("recordsLoaded", null, { records: data.Records, serverResponse: data });
        },

        _onRowInserted: function ($row, isNewRow) {
            this._trigger("rowInserted", null, { row: $row, record: $row.data('record'), isNewRow: isNewRow });
        },

        _onRowsRemoved: function ($rows, reason) {
            this._trigger("rowsRemoved", null, { rows: $rows, reason: reason });
        },

        _onCloseRequested: function () {
            this._trigger("closeRequested", null, {});
        }

    });

} (jQuery));

/************************************************************************
* Some UTULITY methods used by jTable                                   *
*************************************************************************/
(function ($) {

    $.extend(true, $.hik.jtable.prototype, {

        /* Gets property value of an object recursively.
        *************************************************************************/
        _getPropertyOfObject: function (obj, propName) {
            if (propName.indexOf('.') < 0) {
                return obj[propName];
            } else {
                var preDot = propName.substring(0, propName.indexOf('.'));
                var postDot = propName.substring(propName.indexOf('.') + 1);
                return this._getPropertyOfObject(obj[preDot], postDot);
            }
        },

        /* Sets property value of an object recursively.
        *************************************************************************/
        _setPropertyOfObject: function (obj, propName, value) {
            var self = this;

            if (propName.indexOf('.') < 0) {
                obj[propName] = value;
            } else {
                var preDot = propName.substring(0, propName.indexOf('.'));
                var postDot = propName.substring(propName.indexOf('.') + 1);
                self._setPropertyOfObject(obj[preDot], postDot, value);
            }
        },

        /* Inserts a value to an array if it does not exists in the array.
        *************************************************************************/
        _insertToArrayIfDoesNotExists: function (array, value) {
            if ($.inArray(value, array) < 0) {
                array.push(value);
            }
        },

        /* Finds index of an element in an array according to given comparision function
        *************************************************************************/
        _findIndexInArray: function (value, array, compareFunc) {

            //If not defined, use default comparision
            if (!compareFunc) {
                compareFunc = function (a, b) {
                    return a == b;
                };
            }

            for (var i = 0; i < array.length; i++) {
                if (compareFunc(value, array[i])) {
                    return i;
                }
            }

            return -1;
        },

        /* Normalizes a number between given bounds or sets to a defaultValue
        *  if it is undefined
        *************************************************************************/
        _normalizeNumber: function (number, min, max, defaultValue) {
            if (number == undefined || number == null) {
                return defaultValue;
            }

            if (number < min) {
                return min;
            }

            if (number > max) {
                return max;
            }

            return number;
        },

        /* Formats a string just like string.format in c#.
        *  Example:
        *  _formatString('Hello {0}','Halil') = 'Hello Halil'
        *************************************************************************/
        _formatString: function () {
            if (arguments.length == 0) {
                return null;
            }

            var str = arguments[0];
            for (var i = 1; i < arguments.length; i++) {
                var placeHolder = '{' + (i - 1) + '}';
                str = str.replace(placeHolder, arguments[i]);
            }

            return str;
        },
        
        //Logging methods ////////////////////////////////////////////////////////

        _logDebug: function (text) {
            if (!console) {
                return;
            }

            console.log('jTable DEBUG: ' + text);
        },

        _logInfo: function (text) {
            if (!console) {
                return;
            }

            console.log('jTable INFO: ' + text);
        },

        _logWarn: function (text) {
            if (!console) {
                return;
            }

            console.log('jTable WARNING: ' + text);
        },

        _logError: function (text) {
            if (!console) {
                return;
            }

            console.log('jTable ERROR: ' + text);
        }

    });

})(jQuery);

/************************************************************************
* FORMS extension for jTable (base for edit/create forms)               *
*************************************************************************/
(function ($) {

    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Submits a form asynchronously using AJAX.
        *  This method is needed, since form submitting logic can be overrided
        *  by extensions.
        *************************************************************************/
        _submitFormUsingAjax: function (url, formData, success, error) {
            this._performAjaxCall(url, formData, true, success, error);
        },

        /* Creates label for an input element.
        *************************************************************************/
        _createInputLabelForRecordField: function (fieldName) {
            //TODO: May create label tag instead of a div.
            return $('<div class="jtable-input-label">' + this.options.fields[fieldName].title + '</div>');
        },

        /* Creates an input element according to field type.
        *************************************************************************/
        _createInputForRecordField: function (fieldName, value, record) {

            //Get the field
            var field = this.options.fields[fieldName];

            //If value if undefined, use defaultValue of the field
            if (value == undefined) {
                value = field.defaultValue;
            }

            //Use custom function if supplied
            if (field.input) {
                var $input = $(field.input({ value: value, record: record }));

                //Add id attribute if does not exists
                if (!$input.attr('id')) {
                    $input.attr('id', 'Edit-' + fieldName);
                }

                return $input;
            }

            //Create input according to field type
            if (field.type == 'date') {
                return this._createDateInputForField(field, fieldName, value);
            } else if (field.type == 'textarea') {
                return this._createTextAreaForField(field, fieldName, value);
            } else if (field.type == 'password') {
                return this._createPasswordInputForField(field, fieldName, value);
            } else if (field.type == 'checkbox') {
                return this._createCheckboxForField(field, fieldName, value);
            } else if (field.options) {
                if (field.type == 'radiobutton') {
                    return this._createRadioButtonListForField(field, fieldName, value);
                } else {
                    return this._createDropDownListForField(field, fieldName, value);
                }
            } else {
                return this._createTextInputForField(field, fieldName, value);
            }
        },

        //Creates a hidden input element with given name and value.
        _createInputForHidden: function (fieldName, value) {
            if (value == undefined || value == null) {
                value = "";
            }

            return $('<input type="hidden" value="' + value + '" name="' + fieldName + '" id="Edit-' + fieldName + '"></input>');
        },

        /* Creates a date input for a field.
        *************************************************************************/
        _createDateInputForField: function (field, fieldName, value) {
            var $input = $('<input class="' + field.inputClass + '" id="Edit-' + fieldName + '" type="text"' + (value != undefined ? 'value="' + value + '"' : '') + ' name="' + fieldName + '"></input>');
            var displayFormat = field.displayFormat || this.options.defaultDateFormat;
            $input.datepicker({ dateFormat: displayFormat });
            return $('<div class="jtable-input jtable-date-input"></div>').append($input);
        },

        /* Creates a standart textbox for a field.
        *************************************************************************/
        _createTextAreaForField: function (field, fieldName, value) {
            return $('<div class="jtable-input jtable-textarea-input"><textarea class="' + field.inputClass + '" id="Edit-' + fieldName + '" name="' + fieldName + '">' + (value || '') + '</textarea></div>');
        },

        /* Creates a standart textbox for a field.
        *************************************************************************/
        _createTextInputForField: function (field, fieldName, value) {
            return $('<div class="jtable-input jtable-text-input"><input class="' + field.inputClass + '" id="Edit-' + fieldName + '" type="text"' + (value != undefined ? 'value="' + value + '"' : '') + ' name="' + fieldName + '"></input></div>');
        },

        /* Creates a password input for a field.
        *************************************************************************/
        _createPasswordInputForField: function (field, fieldName, value) {
            return $('<div class="jtable-input jtable-password-input"><input class="' + field.inputClass + '" id="Edit-' + fieldName + '" type="password"' + (value != undefined ? 'value="' + value + '"' : '') + ' name="' + fieldName + '"></input></div>');
        },

        /* Creates a checkboxfor a field.
        *************************************************************************/
        _createCheckboxForField: function (field, fieldName, value) {
            var self = this;

            //If value is undefined, get unchecked state's value
            if (value == undefined) {
                value = value || self._getCheckBoxPropertiesForFieldByState(fieldName, false).Value;
            }

            //Create a container div
            var $containerDiv = $('<div class="jtable-input jtable-checkbox-input"></div>');

            //Create checkbox and check if needed
            var $checkBox = $('<input class="' + field.inputClass + '" id="Edit-' + fieldName + '" type="checkbox" name="' + fieldName + '" value="' + value + '" />').appendTo($containerDiv);

            //Create display text of checkbox for current state
            var $textSpan = $('<span>' + (field.formText || self._getCheckBoxTextForFieldByValue(fieldName, value)) + '</span>').appendTo($containerDiv);

            //Check the checkbox if it's value is checked-value
            if (self._getIsCheckBoxSelectedForFieldByValue(fieldName, value)) {
                $checkBox.attr('checked', 'checked');
            }

            //This method sets checkbox's value and text according to state of the checkbox
            var refreshCheckBoxValueAndText = function () {
                var checkboxProps = self._getCheckBoxPropertiesForFieldByState(fieldName, $checkBox.is(':checked'));
                $checkBox.attr('value', checkboxProps.Value);
                $textSpan.html(field.formText || checkboxProps.DisplayText);
            };

            //Register to click event to change display text when state of checkbox is changed.
            $checkBox.click(function () {
                refreshCheckBoxValueAndText();
            });

            //Change checkbox state when clicked to text
            if (field.setOnTextClick != false) {
                $textSpan.addClass('jtable-option-text-clickable');
                $textSpan.click(function () {
                    if ($checkBox.is(':checked')) {
                        $checkBox.attr('checked', false);
                    } else {
                        $checkBox.attr('checked', true);
                    }

                    refreshCheckBoxValueAndText();
                });
            }

            return $containerDiv;
        },

        /* Creates a drop down list (combobox) input element for a field.
        *************************************************************************/
        _createDropDownListForField: function (field, fieldName, value) {
            //Create a container div
            var $containerDiv = $('<div class="jtable-input jtable-dropdown-input"></div>');

            //Create select element
            var $select = $('<select class="' + field.inputClass + '" id="Edit-' + fieldName + '" name=' + fieldName + '></select>').appendTo($containerDiv);

            //add options
            var options = this._getOptionsWithCaching(fieldName);
            $.each(options, function (propName, propValue) {
                $select.append('<option value="' + propName + '"' + (propName == value ? ' selected="selected"' : '') + '>' + propValue + '</option>');
            });

            return $containerDiv;
        },
        /* Creates a radio button list for a field.
        *************************************************************************/
        _createRadioButtonListForField: function (field, fieldName, value) {
            //Create a container div
            var $containerDiv = $('<div class="jtable-input jtable-radiobuttonlist-input"></div>');

            //create radio buttons
            var options = this._getOptionsWithCaching(fieldName);
            var radioButtonIndex = 0;
            $.each(options, function (propName, propValue) {
                var $radioButtonDiv = $('<div class="jtable-radio-input"></div>').appendTo($containerDiv);
                var $radioButton = $('<input type="radio" id="Edit-' + fieldName + (radioButtonIndex++) + '" class="' + field.inputClass + '" name="' + fieldName + '" value="' + propName + '"' + (propName == value ? ' checked="true"' : '') + ' />').appendTo($radioButtonDiv);
                var $textSpan = $('<span>' + propValue + '</span>').appendTo($radioButtonDiv);

                //Change checkbox state when clicked to text
                if (field.setOnTextClick != false) {
                    $textSpan.addClass('jtable-option-text-clickable');
                    $textSpan.click(function () {
                        if (!$radioButton.is(':checked')) {
                            $radioButton.attr('checked', true);
                        }
                    });
                }
            });

            return $containerDiv;
        },

        /* Gets display text for a checkbox field.
        *************************************************************************/
        _getCheckBoxTextForFieldByValue: function (fieldName, value) {
            return this.options.fields[fieldName].values[value];
        },

        /* Returns true if given field's value must be checked state.
        *************************************************************************/
        _getIsCheckBoxSelectedForFieldByValue: function (fieldName, value) {
            return (this._createCheckBoxStateArrayForFieldWithCaching(fieldName)[1].Value.toString() == value.toString());
        },

        /* Gets an object for a checkbox field that has Value and DisplayText
        *  properties.
        *************************************************************************/
        _getCheckBoxPropertiesForFieldByState: function (fieldName, checked) {
            return this._createCheckBoxStateArrayForFieldWithCaching(fieldName)[(checked ? 1 : 0)];
        },

        /* Calls _createCheckBoxStateArrayForField with caching.
        *************************************************************************/
        _createCheckBoxStateArrayForFieldWithCaching: function (fieldName) {
            var cacheKey = 'checkbox_' + fieldName;
            if (!this._cache[cacheKey]) {

                this._cache[cacheKey] = this._createCheckBoxStateArrayForField(fieldName);
            }

            return this._cache[cacheKey];
        },

        /* Creates a two element array of objects for states of a checkbox field.
        *  First element for unchecked state, second for checked state.
        *  Each object has two properties: Value and DisplayText
        *************************************************************************/
        _createCheckBoxStateArrayForField: function (fieldName) {
            var stateArray = [];
            var currentIndex = 0;
            $.each(this.options.fields[fieldName].values, function (propName, propValue) {
                if (currentIndex++ < 2) {
                    stateArray.push({ 'Value': propName, 'DisplayText': propValue });
                }
            });

            return stateArray;
        },

        /* Gets options from cache if exists, else downloads and caches.
        *************************************************************************/
        _getOptionsWithCaching: function (fieldName) {
            var cacheKey = 'options_' + fieldName;
            if (!this._cache[cacheKey]) {
                var optionsSource = this.options.fields[fieldName].options;
                //Build options according to it's source type
                if (typeof optionsSource == 'string') {
                    //It is an Url to rownload options
                    this._cache[cacheKey] = this._downloadOptions(fieldName, optionsSource);
                } else if (jQuery.isArray(optionsSource)) {
                    //It is an array of options
                    this._cache[cacheKey] = this._buildOptionsFromArray(optionsSource);
                } else {
                    //It is an object that it's properties are options, so use directly this object
                    this._cache[cacheKey] = optionsSource;
                }
            }

            return this._cache[cacheKey];
        },

        /* Download options for a field from server.
        *************************************************************************/
        _downloadOptions: function (fieldName, url) {
            var self = this;

            var options = {};
            self._performAjaxCall(
                url,
                undefined,
                false,
                function (data) { //success
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        return;
                    }

                    //Get options from incoming data
                    for (var i = 0; i < data.Options.length; i++) {
                        options[data.Options[i].Value] = data.Options[i].DisplayText;
                    }
                },
                function () {
                    var errMessage = self._formatString(self.options.messages.cannotLoadOptionsFor, fieldName);
                    self._showError(errMessage);
                });

            return options;
        },

        /* Creates an options object (that it's property is value, value is displaytext)
        *  from a simple array.
        *************************************************************************/
        _buildOptionsFromArray: function (optionsArray) {
            var options = {};
            for (var i = 0; i < optionsArray.length; i++) {
                options[optionsArray[i]] = optionsArray[i];
            }

            return options;
        },

        /* Sets enabled/disabled state of a dialog button.
        *************************************************************************/
        _setEnabledOfDialogButton: function ($button, enabled, buttonText) {
            if (!$button) {
                return;
            }

            if (enabled != false) {
                $button.removeAttr('disabled').removeClass('ui-state-disabled');
            } else {
                $button.attr('disabled', 'disabled').addClass('ui-state-disabled');
            }

            if (buttonText) {
                $button.find('span').text(buttonText);
            }
        }

    });

})(jQuery);

/************************************************************************
* CREATE RECORD extension for jTable                                    *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {

            //Events
            recordAdded: function (event, data) { },

            //Localization
            messages: {
                addNewRecord: '+ Add new record'
            }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _$addRecordDiv: null, //Reference to the adding new record dialog div (jQuery object)

        /************************************************************************
        * CONSTRUCTOR                                                           *
        *************************************************************************/

        /* Overrides base method to do create-specific constructions.
        *************************************************************************/
        _create: function () {
            base._create.apply(this, arguments);
            this._createAddRecordDialogDiv();
        },

        /* Creates and prepares add new record dialog div
        *************************************************************************/
        _createAddRecordDialogDiv: function () {
            var self = this;

            //Check if createAction is supplied
            if (!self.options.actions.createAction) {
                return;
            }

            //Create a div for dialog and add to container element
            self._$addRecordDiv = $('<div></div>').appendTo(self._$mainContainer);

            //Prepare dialog
            self._$addRecordDiv.dialog({
                autoOpen: false,
                show: self.options.dialogShowEffect,
                hide: self.options.dialogHideEffect,
                width: 'auto',
                minWidth: '300',
                modal: true,
                title: self.options.messages.addNewRecord,
                buttons:
                        [{ //Cancel button
                            text: self.options.messages.cancel,
                            click: function () {
                                self._$addRecordDiv.dialog('close');
                            }
                        }, { //Save button
                            id: 'AddRecordDialogSaveButton',
                            text: self.options.messages.save,
                            click: function () {
                                var $saveButton = $('#AddRecordDialogSaveButton');
                                var $addRecordForm = self._$addRecordDiv.find('form');

                                if (self._trigger("formSubmitting", null, { form: $addRecordForm, formType: 'create' }) != false) {
                                    self._setEnabledOfDialogButton($saveButton, false, self.options.messages.saving);
                                    self._saveAddRecordForm($addRecordForm, $saveButton);
                                }
                            }
                        }],
                close: function () {
                    var $addRecordForm = self._$addRecordDiv.find('form').first();
                    var $saveButton = $('#AddRecordDialogSaveButton');
                    self._trigger("formClosed", null, { form: $addRecordForm, formType: 'create' });
                    self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                    $addRecordForm.remove();
                }
            });

            //If not 'add record button' supplied, create a new one.
            if (!self.options.addRecordButton) {
                self.options.addRecordButton = self._createAddRecordButton();
            }

            //Bind click event to show dialog form
            self.options.addRecordButton.click(function (e) {
                e.preventDefault();
                self._showAddRecordForm();
            });
        },

        /* Creates and returns 'add new record' button/link.
        *************************************************************************/
        _createAddRecordButton: function () {
            return $('<span class="jtable-add-record"><a href="#">' + this.options.messages.addNewRecord + '</a></span>').appendTo(this._$bottomPanel);
        },

        /************************************************************************
        * PUBLIC METHODS                                                        *
        *************************************************************************/

        /* Shows add new record dialog form.
        *************************************************************************/
        showCreateForm: function () {
            this._showAddRecordForm();
        },

        /* Adds a new record to the table (optionally to the server also)
        *************************************************************************/
        addRecord: function (options) {
            var self = this;
            options = $.extend({
                clientOnly: false,
                animationsEnabled: self.options.animationsEnabled,
                url: self.options.actions.createAction,
                success: function () { },
                error: function () { }
            }, options);

            if (!options.record) {
                self._logWarn('options parameter in addRecord method must contain a record property.');
                return;
            }

            if (options.clientOnly) {
                self._addRowToTable(self._createRowFromRecord(options.record), null, true, options.animationsEnabled);
                options.success();
                console.log("clieneeeeeee");
                return;
            }

            self._submitFormUsingAjax(
                options.url,
                $.param(options.record),
                function (data) {
                    //Check for errors
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        options.error(data);
                        return;
                    }

                    self._onRecordAdded(data);
                    self._addRowToTable(self._createRowFromRecord(data.Record), null, true, options.animationsEnabled);
                    options.success(data);
                },
                function () {
                    self._showError(self.options.messages.serverCommunicationError);
                    options.error();
                });
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Shows add new record dialog form.
        *************************************************************************/
        _showAddRecordForm: function () {
            var self = this;

            //Create add new record form
            var $addRecordForm = $('<form id="jtable-create-form" class="jtable-dialog-form jtable-create-form" action="' + self.options.actions.createAction + '" method="POST"></form>');

            //Create input elements
            for (var i = 0; i < self._fieldList.length; i++) {

                //Do not create input for fields that are not creatable
                if (self.options.fields[self._fieldList[i]].create == false) {
                    continue;
                }

                if (self.options.fields[self._fieldList[i]].type == 'hidden') {
                    $addRecordForm.append(self._createInputForHidden(self._fieldList[i], self.options.fields[self._fieldList[i]].defaultValue));
                    continue;
                }

                //Create a container div for this input field and add to form
                var $fieldContainer = $('<div class="jtable-input-field-container"></div>').appendTo($addRecordForm);

                //Create a label for input
                $fieldContainer.append(self._createInputLabelForRecordField(self._fieldList[i]));

                //Create input element
                $fieldContainer.append(self._createInputForRecordField(self._fieldList[i]));
            }

            //Open the form
            self._$addRecordDiv.append($addRecordForm).dialog('open');
            self._trigger("formCreated", null, { form: $addRecordForm, formType: 'create' });
        },

        /* Saves new added record to the server and updates table.
        *************************************************************************/
        _saveAddRecordForm: function ($addRecordForm, $saveButton) {
            var self = this;

            //Make an Ajax call to update record
            $addRecordForm.data('submitting', true);

            self._submitFormUsingAjax(
                $addRecordForm.attr('action'),
                $addRecordForm.serialize(),
                function (data) {
                    //Check for errors
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                        return;
                    }

                    self._onRecordAdded(data);
                    self._addRowToTable(self._createRowFromRecord(data.Record), null, true);
                    self._$addRecordDiv.dialog("close");
                },
                function () {
                    self._showError(self.options.messages.serverCommunicationError);
                    self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                });
        },

        _onRecordAdded: function (data) {
            this._trigger("recordAdded", null, { record: data.Record, serverResponse: data });
        }

    });

})(jQuery);

/************************************************************************
* EDIT RECORD extension for jTable                                      *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create,
        _addColumnsToHeaderRow: $.hik.jtable.prototype._addColumnsToHeaderRow,
        _addCellsToRowUsingRecord: $.hik.jtable.prototype._addCellsToRowUsingRecord
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {

            //Events
            recordUpdated: function (event, data) { },
            rowUpdated: function (event, data) { },

            //Localization
            messages: {
                editRecord: 'Edit Record'
            }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _$editDiv: null, //Reference to the editing dialog div (jQuery object)
        _$editingRow: null, //Reference to currently editing row (jQuery object)

        /************************************************************************
        * CONSTRUCTOR AND INITIALIZATION METHODS                                *
        *************************************************************************/

        /* Overrides base method to do editing-specific constructions.
        *************************************************************************/
        _create: function () {
            base._create.apply(this, arguments);
            this._createEditDialogDiv();
        },

        /* Creates and prepares edit dialog div
        *************************************************************************/
        _createEditDialogDiv: function () {
            var self = this;

            //Create a div for dialog and add to container element
            self._$editDiv = $('<div></div>').appendTo(self._$mainContainer);

            //Prepare dialog
            self._$editDiv.dialog({
                autoOpen: false,
                show: self.options.dialogShowEffect,
                hide: self.options.dialogHideEffect,
                width: 'auto',
                minWidth: '300',
                modal: true,
                title: self.options.messages.editRecord,
                buttons:
                        [{  //cancel button
                            text: self.options.messages.cancel,
                            click: function () {
                                self._$editDiv.dialog('close');
                            }
                        }, { //save button
                            id: 'EditDialogSaveButton',
                            text: self.options.messages.save,
                            click: function () {
                                var $saveButton = self._$editDiv.find('#EditDialogSaveButton');
                                var $editForm = self._$editDiv.find('form');
                                if (self._trigger("formSubmitting", null, { form: $editForm, formType: 'edit' }) != false) {
                                    self._setEnabledOfDialogButton($saveButton, false, self.options.messages.saving);
                                    self._saveEditForm($editForm, $saveButton);
                                }
                            }
                        }],
                close: function () {
                    var $editForm = self._$editDiv.find('form:first');
                    var $saveButton = $('#EditDialogSaveButton');
                    self._trigger("formClosed", null, { form: $editForm, formType: 'edit' });
                    self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                    $editForm.remove();
                }
            });
        },

        /************************************************************************
        * PUNLIC METHODS                                                        *
        *************************************************************************/

        /* Updates a record on the table (optionally on the server also)
        *************************************************************************/
        updateRecord: function (options) {
            var self = this;
            options = $.extend({
                clientOnly: false,
                animationsEnabled: self.options.animationsEnabled,
                url: self.options.actions.updateAction,
                success: function () { },
                error: function () { }
            }, options);

            var key = options.record[self._keyField];
            if (!options.record || !key) {
                self._logWarn('options parameter in updateRecord method must contain a record that contains the key field property.');
                return;
            }

            var $updatingRow = self.getRowByKey(key);
            if ($updatingRow == null) {
                self._logWarn('Can not found any row by key: ' + key);
                return;
            }

            if (options.clientOnly) {
                $.extend($updatingRow.data('record'), options.record);
                self._updateRowTexts($updatingRow);

                self._onRecordUpdated($updatingRow, null);

                if (options.animationsEnabled) {
                    self._showUpdateAnimationForRow($updatingRow);
                }

                options.success();
                return;
            }

            self._submitFormUsingAjax(
                options.url,
                $.param(options.record),
                function (data) {
                    //Check for errors
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        options.error(data);
                        return;
                    }

                    $.extend($updatingRow.data('record'), options.record);
                    self._updateRowTexts($updatingRow);

                    self._onRecordUpdated($updatingRow, data);

                    if (options.animationsEnabled) {
                        self._showUpdateAnimationForRow($updatingRow);
                    }

                    options.success(data);
                },
                function () {
                    self._showError(self.options.messages.serverCommunicationError);
                    options.error();
                });
        },

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides base method to add a 'editing column cell' to header row.
        *************************************************************************/
        _addColumnsToHeaderRow: function ($tr) {
            base._addColumnsToHeaderRow.apply(this, arguments);
            if (this.options.actions.updateAction != undefined) {
                $tr.append(this._createEmptyCommandHeader());
            }
        },

        /* Overrides base method to add a 'edit command cell' to a row.
        *************************************************************************/
        _addCellsToRowUsingRecord: function ($row) {
            var self = this;
            base._addCellsToRowUsingRecord.apply(this, arguments);
            if (self.options.actions.updateAction != undefined) {
                var $editCell = $('<td class="jtable-command-column"></td>').appendTo($row);
                $('<button class="jtable-command-button jtable-edit-command-button" title="' + self.options.messages.editRecord + '"><span>' + self.options.messages.editRecord + '</span></button>')
                    .appendTo($editCell)
                    .click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        self._showEditForm($row);
                    });
            }
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Shows edit form for a row.
        *************************************************************************/
        _showEditForm: function ($tableRow) {
            var self = this;
            var record = $tableRow.data('record');

            //Create edit form
            var $editForm = $('<form id="jtable-edit-form" class="jtable-dialog-form jtable-edit-form" action="' + self.options.actions.updateAction + '" method="POST"></form>');

            //Create input fields
            for (var i = 0; i < self._fieldList.length; i++) {

                //Create hidden input for 'key' field
                if (self.options.fields[self._fieldList[i]].key == true) {
                    $editForm.append(self._createInputForHidden(self._fieldList[i], record[self._fieldList[i]]));
                    continue;
                }

                //Do not create element for non-editable fields
                if (self.options.fields[self._fieldList[i]].edit == false) {
                    continue;
                }

                //Hidden field
                if (self.options.fields[self._fieldList[i]].type == 'hidden') {
                    $editForm.append(self._createInputForHidden(self._fieldList[i], record[self._fieldList[i]]));
                    continue;
                }

                //Create a container div for this input field and add to form
                var $fieldContainer = $('<div class="jtable-input-field-container"></div>').appendTo($editForm);

                //Create a label for input
                $fieldContainer.append(self._createInputLabelForRecordField(self._fieldList[i]));

                //Create input element with it's current value
                var currentValue = self._getValueForRecordField(record, self._fieldList[i]);
                $fieldContainer.append(self._createInputForRecordField(self._fieldList[i], currentValue, record));
            }

            //Open dialog
            self._$editingRow = $tableRow;
            self._$editDiv.append($editForm).dialog('open');
            self._trigger("formCreated", null, { form: $editForm, formType: 'edit', record: record });
        },

        /* Saves editing form to the server and updates the record on the table.
        *************************************************************************/
        _saveEditForm: function ($editForm, $saveButton) {
            var self = this;
            self._submitFormUsingAjax(
                $editForm.attr('action'),
                $editForm.serialize(),
                function (data) {
                    //Check for errors
                    if (data.Result != 'OK') {
                        self._showError(data.Message);
                        self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                        return;
                    }

                    var $editingRow = self._$editingRow;

                    self._updateRecordValuesFromEditForm($editingRow.data('record'), $editForm);
                    self._updateRowTexts($editingRow);

                    self._onRecordUpdated($editingRow, data);

                    if (self.options.animationsEnabled) {
                        self._showUpdateAnimationForRow($editingRow);
                    }

                    self._$editDiv.dialog("close");
                },
                function () {
                    self._showError(self.options.messages.serverCommunicationError);
                    self._setEnabledOfDialogButton($saveButton, true, self.options.messages.save);
                });
        },

        /* Updates values of a record from given edit form
        *************************************************************************/
        _updateRecordValuesFromEditForm: function (record, $form) {
            for (var i = 0; i < this._fieldList.length; i++) {
                var fieldName = this._fieldList[i];
                var field = this.options.fields[fieldName];

                //Do not update non-editable fields
                if (field.edit == false) {
                    continue;
                }

                //Get field name and the input element of this field in the form
                var $inputElement = $form.find('[name="' + fieldName + '"]');

                //Update field in record according to it's type
                if (field.type == 'date') {
                    var displayFormat = field.displayFormat || this.options.defaultDateFormat;
                    try {
                        var date = $.datepicker.parseDate(displayFormat, $inputElement.val());
                        record[fieldName] = '/Date(' + date.getTime() + ')/';
                    } catch (e) {
                        //TODO: Handle incorrect/different date formats
                        record[fieldName] = '/Date(' + (new Date()).getTime() + ')/';
                    }
                } else if (field.options && field.type == 'radiobutton') {
                    var $checkedElement = $inputElement.filter('[checked="true"]');
                    if ($checkedElement.length) {
                        record[fieldName] = $checkedElement.val();
                    } else {
                        record[fieldName] = undefined;
                    }
                } else {
                    record[fieldName] = $inputElement.val();
                }
            }
        },

        /* Gets text for a field of a record according to it's type.
        *************************************************************************/
        _getValueForRecordField: function (record, fieldName) {
            var field = this.options.fields[fieldName];
            var fieldValue = record[fieldName];
            if (field.type == 'date') {
                return this._getDisplayTextForDateRecordField(field, fieldValue);
            } else {
                return fieldValue;
            }
        },

        /* Updates cells of a table row's text values from row's record values.
        *************************************************************************/
        _updateRowTexts: function ($tableRow) {
            var record = $tableRow.data('record');
            var $columns = $tableRow.find('td');
            for (var i = 0; i < this._columnList.length; i++) {
                var displayItem = this._getDisplayTextForRecordField(record, this._columnList[i]);
                $columns.eq(this._firstDataColumnOffset + i).html(displayItem || '');
            }

            this._onRowUpdated($tableRow);
        },

        /* Shows 'updated' animation for a table row.
        *************************************************************************/
        _showUpdateAnimationForRow: function ($tableRow) {
            $tableRow.stop(true, true).addClass('jtable-row-updated', 'slow', '', function () {
                $tableRow.removeClass('jtable-row-updated', 5000);
            });
        },

        /************************************************************************
        * EVENT RAISING METHODS                                                 *
        *************************************************************************/

        _onRowUpdated: function ($row) {
            this._trigger("rowUpdated", null, { row: $row, record: $row.data('record') });
        },

        _onRecordUpdated: function ($row, data) {
            this._trigger("recordUpdated", null, { record: $row.data('record'), row: $row, serverResponse: data });
        }

    });

})(jQuery);

/************************************************************************
* DELETION extension for jTable                                         *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create,
        _addColumnsToHeaderRow: $.hik.jtable.prototype._addColumnsToHeaderRow,
        _addCellsToRowUsingRecord: $.hik.jtable.prototype._addCellsToRowUsingRecord
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {

            //Options
            deleteConfirmation: true,

            //Events
            recordDeleted: function (event, data) { },

            //Localization
            messages: {
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                deleteText: 'Delete',
                deleting: 'Deleting',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _$deleteRecordDiv: null, //Reference to the adding new record dialog div (jQuery object)
        _$deletingRow: null, //Reference to currently deleting row (jQuery object)

        /************************************************************************
        * CONSTRUCTOR                                                           *
        *************************************************************************/

        /* Overrides base method to do deletion-specific constructions.
        *************************************************************************/
        _create: function () {
            base._create.apply(this, arguments);
            this._createDeleteDialogDiv();
        },

        /* Creates and prepares delete record confirmation dialog div.
        *************************************************************************/
        _createDeleteDialogDiv: function () {
            var self = this;

            //Create div element for delete confirmation dialog
            self._$deleteRecordDiv = $('<div><p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span><span class="jtable-delete-confirm-message"></span></p></div>').appendTo(self._$mainContainer);

            //Prepare dialog
            self._$deleteRecordDiv.dialog({
                autoOpen: false,
                show: self.options.dialogShowEffect,
                hide: self.options.dialogHideEffect,
                modal: true,
                title: self.options.messages.areYouSure,
                buttons:
                        [{  //cancel button
                            text: self.options.messages.cancel,
                            click: function () {
                                self._$deleteRecordDiv.dialog("close");
                            }
                        }, {//delete button
                            id: 'DeleteDialogButton',
                            text: self.options.messages.deleteText,
                            click: function () {
                                var $deleteButton = $('#DeleteDialogButton');
                                self._setEnabledOfDialogButton($deleteButton, false, self.options.messages.deleting);
                                self._deleteRecordFromServer(
                                    self._$deletingRow,
                                    function () {
                                        self._removeRowsFromTableWithAnimation(self._$deletingRow);
                                        self._$deleteRecordDiv.dialog('close');
                                    },
                                    function (message) { //error
                                        self._showError(message);
                                        self._setEnabledOfDialogButton($deleteButton, true, self.options.messages.deleteText);
                                    }
                                );
                            }
                        }],
                close: function () {
                    var $deleteButton = $('#DeleteDialogButton');
                    self._setEnabledOfDialogButton($deleteButton, true, self.options.messages.deleteText);
                }
            });
        },

        /************************************************************************
        * PUBLIC METHODS                                                        *
        *************************************************************************/

        /* This method is used to delete one or more rows from server and the table.
        *************************************************************************/
        deleteRows: function ($rows) {
            var self = this;

            //If no rows specified, or jTable is already busy, no action.
            if ($rows.length <= 0 || self._isBusy()) {
                return;
            }

            //Deleting just one row
            if ($rows.length == 1) {
                self._deleteRecordFromServer(
                    $rows,
                    function () { //success
                        self._removeRowsFromTableWithAnimation($rows);
                    },
                    function (message) { //error
                        self._showError(message);
                    }
                );

                return;
            }

            //Deleting multiple rows
            self._showBusy(self._formatString(self.options.messages.deleteProggress, 0, $rows.length));

            //This method checks if deleting of all records is completed
            var completedCount = 0;
            var isCompleted = function () {
                return (completedCount >= $rows.length);
            };

            //This method is called when deleting of all records completed
            var completed = function () {
                var $deletedRows = $rows.filter('.jtable-row-ready-to-remove');
                if ($deletedRows.length < $rows.length) {
                    self._showError(self._formatString(self.options.messages.canNotDeletedRecords, $rows.length - $deletedRows.length, $rows.length));
                }

                if ($deletedRows.length > 0) {
                    self._removeRowsFromTableWithAnimation($deletedRows);
                }

                self._hideBusy();
            };

            //Delete all rows
            var deletedCount = 0;
            $rows.each(function () {
                var $row = $(this);
                self._deleteRecordFromServer(
                    $row,
                    function () { //success
                        ++deletedCount; ++completedCount;
                        $row.addClass('jtable-row-ready-to-remove');
                        self._showBusy(self._formatString(self.options.messages.deleteProggress, deletedCount, $rows.length));
                        if (isCompleted()) {
                            completed();
                        }
                    },
                    function () { //error
                        ++completedCount;
                        if (isCompleted()) {
                            completed();
                        }
                    }
                );
            });
        },

        /* Deletes a record from the table (optionally from the server also).
        *************************************************************************/
        deleteRecord: function (options) {
            var self = this;
            options = $.extend({
                clientOnly: false,
                animationsEnabled: self.options.animationsEnabled,
                url: self.options.actions.deleteAction,
                success: function () { },
                error: function () { }
            }, options);

            if (!options.key) {
                self._logWarn('options parameter in deleteRecord method must contain a record key.');
                return;
            }

            var $deletingRow = self.getRowByKey(options.key);
            if ($deletingRow == null) {
                self._logWarn('Can not found any row by key: ' + options.key);
                return;
            }

            if (options.clientOnly) {
                self._removeRowsFromTableWithAnimation($deletingRow, options.animationsEnabled);
                options.success();
                return;
            }

            self._deleteRecordFromServer(
                    $deletingRow,
                    function (data) { //success
                        self._removeRowsFromTableWithAnimation($deletingRow, options.animationsEnabled);
                        options.success(data);
                    },
                    function (message) { //error
                        self._showError(message);
                        options.error(message);
                    }
                );
        },

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides base method to add a 'deletion column cell' to header row.
        *************************************************************************/
        _addColumnsToHeaderRow: function ($tr) {
            base._addColumnsToHeaderRow.apply(this, arguments);
            if (this.options.actions.deleteAction != undefined) {
                $tr.append(this._createEmptyCommandHeader());
            }
        },

        /* Overrides base method to add a 'delete command cell' to a row.
        *************************************************************************/
        _addCellsToRowUsingRecord: function ($row) {
            base._addCellsToRowUsingRecord.apply(this, arguments);

            var self = this;
            if (self.options.actions.deleteAction != undefined) {
                var $deleteCell = $('<td class="jtable-command-column"></td>').appendTo($row);
                $('<button class="jtable-command-button jtable-delete-command-button" title="' + self.options.messages.deleteText + '"><span>' + self.options.messages.deleteText + '</span></button>')
                    .appendTo($deleteCell)
                    .click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        self._deleteButtonClickedForRow($row);
                    });
            }
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* This method is called when user clicks delete button on a row.
        *************************************************************************/
        _deleteButtonClickedForRow: function ($row) {
            var self = this;

            var deleteConfirm;
            var deleteConfirmMessage = self.options.messages.deleteConfirmation;

            //If options.deleteConfirmation is function then call it
            if ($.isFunction(self.options.deleteConfirmation)) {
                var data = { row: $row, record: $row.data('record'), deleteConfirm: true, deleteConfirmMessage: deleteConfirmMessage, cancel: false, cancelMessage: null };
                self.options.deleteConfirmation(data);

                //If delete progress is cancelled
                if (data.cancel) {

                    //If a canlellation reason is specified
                    if (data.cancelMessage) {
                        self._showError(data.cancelMessage); //TODO: show warning/stop message instead of error (also show warning/error ui icon)!
                    }

                    return;
                }

                deleteConfirmMessage = data.deleteConfirmMessage;
                deleteConfirm = data.deleteConfirm;
            } else {
                deleteConfirm = self.options.deleteConfirmation;
            }

            if (deleteConfirm != false) {
                //Confirmation
                self._$deleteRecordDiv.find('.jtable-delete-confirm-message').html(deleteConfirmMessage);
                self._showDeleteDialog($row);
            } else {
                //No confirmation
                self._deleteRecordFromServer(
                    $row,
                    function () { //success
                        self._removeRowsFromTableWithAnimation($row);
                    },
                    function (message) { //error
                        self._showError(message);
                    }
                );
            }
        },

        /* Shows delete comfirmation dialog.
        *************************************************************************/
        _showDeleteDialog: function ($row) {
            this._$deletingRow = $row;
            this._$deleteRecordDiv.dialog('open');
        },

        /* Performs an ajax call to server to delete record
        *  and removesd row of record from table if ajax call success.
        *************************************************************************/
        _deleteRecordFromServer: function ($row, success, error) {
            var self = this;

            //Check if it is already being deleted right now
            if ($row.data('deleting') == true) {
                return;
            }

            $row.data('deleting', true);

            var postData = {};
            postData[self._keyField] = $row.data('record')[self._keyField];
            self._performAjaxCall(
                self.options.actions.deleteAction,
                postData,
                true,
                function (data) { //success
                    //Check for errors
                    if (data.Result != 'OK') {
                        $row.data('deleting', false);
                        if (error) {
                            error(data.Message);
                        }

                        return;
                    }

                    self._trigger("recordDeleted", null, { record: $row.data('record'), row: $row, serverResponse: data });

                    if (success) {
                        success(data);
                    }
                },
                function () {
                    $row.data('deleting', false);
                    if (error) {
                        error(self.options.messages.serverCommunicationError);
                    }
                });
        },

        /* Removes a row from table after a 'deleting' animation.
        *************************************************************************/
        _removeRowsFromTableWithAnimation: function ($rows, animationsEnabled) {
            var self = this;

            if (animationsEnabled == undefined) {
                animationsEnabled = self.options.animationsEnabled;
            }

            if (animationsEnabled) {
                //Stop current animation (if does exists) and begin 'deleting' animation.
                $rows.stop(true, true).addClass('jtable-row-deleting', 'slow', '').promise().done(function () {
                    self._removeRowsFromTable($rows, 'deleted');
                });
            } else {
                self._removeRowsFromTable($rows, 'deleted');
            }
        }

    });

})(jQuery);

/************************************************************************
* SELECTING extension for jTable                                        *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create,
        _addColumnsToHeaderRow: $.hik.jtable.prototype._addColumnsToHeaderRow,
        _addCellsToRowUsingRecord: $.hik.jtable.prototype._addCellsToRowUsingRecord,
        _onLoadingRecords: $.hik.jtable.prototype._onLoadingRecords,
        _onRecordsLoaded: $.hik.jtable.prototype._onRecordsLoaded,
        _onRowsRemoved: $.hik.jtable.prototype._onRowsRemoved
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {

            //Options
            selecting: false,
            multiselect: false,
            selectingCheckboxes: false,
            selectOnRowClick: true,

            //Events
            selectionChanged: function (event, data) { }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _selectedRecordIdsBeforeLoad: null, //This array is used to store selected row Id's to restore them after a page refresh (string array).
        _$selectAllCheckbox: null, //Reference to the 'select/deselect all' checkbox (jQuery object)
        _shiftKeyDown: false, //True, if shift key is currently down.

        /************************************************************************
        * CONSTRUCTOR                                                           *
        *************************************************************************/

        /* Overrides base method to do selecting-specific constructions.
        *************************************************************************/
        _create: function () {
            if (this.options.selecting && this.options.selectingCheckboxes) {
                ++this._firstDataColumnOffset;
            }

            this._bindKeyboardEvents();
            
            //Call base method
            base._create.apply(this, arguments);
        },

        /* Registers to keyboard events those are needed for selection
        *************************************************************************/
        _bindKeyboardEvents: function () {
            var self = this;
            //Register to events to set _shiftKeyDown value
            $(document).keydown(function (event) {
                switch (event.which) {
                    case 16: //shift key
                        self._shiftKeyDown = true;
                        break;
                }
            }).keyup(function (event) {
                switch (event.which) {
                    case 16: //shift key
                        self._shiftKeyDown = false;
                        break;
                }
            });
        },

        /************************************************************************
        * PUBLIC METHODS                                                        *
        *************************************************************************/

        /* Gets jQuery selection for currently selected rows.
        *************************************************************************/
        selectedRows: function () {
            return this._getSelectedRows();
        },

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides base method to add a 'select column' to header row.
        *************************************************************************/
        _addColumnsToHeaderRow: function ($tr) {
            if (this.options.selecting && this.options.selectingCheckboxes) {
                if (this.options.multiselect) {
                    $tr.append(this._createSelectAllHeader());
                } else {
                    $tr.append(this._createEmptyCommandHeader());
                }
            }

            base._addColumnsToHeaderRow.apply(this, arguments);
        },

        /* Overrides base method to add a 'delete command cell' to a row.
        *************************************************************************/
        _addCellsToRowUsingRecord: function ($row) {
            if (this.options.selecting) {
                this._makeRowSelectable($row);
            }

            base._addCellsToRowUsingRecord.apply(this, arguments);
        },

        /* Overrides base event to store selection list
        *************************************************************************/
        _onLoadingRecords: function () {
            this._storeSelectionList();
            base._onLoadingRecords.apply(this, arguments);
        },

        /* Overrides base event to restore selection list
        *************************************************************************/
        _onRecordsLoaded: function () {
            this._restoreSelectionList();
            base._onRecordsLoaded.apply(this, arguments);
        },

        /* Overrides base event to check is any selected row is being removed.
        *************************************************************************/
        _onRowsRemoved: function ($rows, reason) {
            if ((reason != 'reloading') && this.options.selecting && ($rows.filter('.jtable-row-selected').length > 0)) {
                this._onSelectionChanged();
            }

            base._onRowsRemoved.apply(this, arguments);
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Creates a header column to select/deselect all rows.
        *************************************************************************/
        _createSelectAllHeader: function () {
            var self = this;

            var $columnHeader = $('<th class="jtable-column-header jtable-column-header-selecting"></th>');
            var $headerContainer = $('<div class="jtable-column-header-container"></div>').appendTo($columnHeader);
            self._$selectAllCheckbox = $('<input type="checkbox" />').appendTo($headerContainer);

            self._$selectAllCheckbox.click(function () {
                if (self._$tableRows.length <= 0) {
                    self._$selectAllCheckbox.attr('checked', false);
                    return;
                }

                var allRows = self._$tableBody.find('tr');
                if (self._$selectAllCheckbox.is(':checked')) {
                    self._selectRows(allRows);
                } else {
                    self._deselectRows(allRows);
                }

                self._onSelectionChanged();
            });

            return $columnHeader;
        },

        /* Stores Id's of currently selected records to _selectedRecordIdsBeforeLoad.
        *************************************************************************/
        _storeSelectionList: function () {
            var self = this;

            if (!self.options.selecting) {
                return;
            }

            self._selectedRecordIdsBeforeLoad = [];
            self._getSelectedRows().each(function () {
                self._selectedRecordIdsBeforeLoad.push($(this).data('record')[self._keyField]);
            });
        },

        /* Selects rows whose Id is in _selectedRecordIdsBeforeLoad;
        *************************************************************************/
        _restoreSelectionList: function () {
            var self = this;

            if (!self.options.selecting) {
                return;
            }

            var selectedRowCount = 0;
            for (var i = 0; i < self._$tableRows.length; ++i) {
                if ($.inArray(self._$tableRows[i].data('record')[self._keyField], self._selectedRecordIdsBeforeLoad) > -1) {
                    self._selectRows(self._$tableRows[i]);
                    ++selectedRowCount;
                }
            }

            if (self._selectedRecordIdsBeforeLoad.length > 0 && self._selectedRecordIdsBeforeLoad.length != selectedRowCount) {
                self._onSelectionChanged();
            }

            self._selectedRecordIdsBeforeLoad = [];
            self._refreshSelectAllCheckboxState();
        },

        /* Gets all selected rows.
        *************************************************************************/
        _getSelectedRows: function () {
            return this._$tableBody.find('.jtable-row-selected');
        },

        /* Adds selectable feature to a row.
        *************************************************************************/
        _makeRowSelectable: function ($row) {
            var self = this;

            //Select/deselect on row click
            if (self.options.selectOnRowClick) {
                $row.click(function () {
                    self._invertRowSelection($row);
                });
            }

            //'select/deselect' checkbox column
            if (self.options.selectingCheckboxes) {
                var $cell = $('<td class="jtable-selecting-column"></td>');
                var $selectCheckbox = $('<input type="checkbox" />').appendTo($cell);

                if (!self.options.selectOnRowClick) {
                    $selectCheckbox.click(function () {
                        self._invertRowSelection($row);
                    });
                }

                $row.append($cell);
            }
        },

        /* Inverts selection state of a single row.
        *************************************************************************/
        _invertRowSelection: function ($row) {
            if ($row.hasClass('jtable-row-selected')) {
                this._deselectRows($row);
            } else {
                //Shift key?
                if (this._shiftKeyDown) {
                    var rowIndex = this._findRowIndex($row);
                    //try to select row and above rows until first selected row
                    var beforeIndex = this._findFirstSelectedRowIndexBeforeIndex(rowIndex) + 1;
                    if (beforeIndex > 0 && beforeIndex < rowIndex) {
                        this._selectRows(this._$tableBody.find('tr').slice(beforeIndex, rowIndex + 1));
                    } else {
                        //try to select row and below rows until first selected row
                        var afterIndex = this._findFirstSelectedRowIndexAfterIndex(rowIndex) - 1;
                        if (afterIndex > rowIndex) {
                            this._selectRows(this._$tableBody.find('tr').slice(rowIndex, afterIndex + 1));
                        } else {
                            //just select this row
                            this._selectRows($row);
                        }
                    }
                } else {
                    this._selectRows($row);
                }
            }

            this._onSelectionChanged();
        },

        /* Search for a selected row (that is before given row index) to up and returns it's index 
        *************************************************************************/
        _findFirstSelectedRowIndexBeforeIndex: function (rowIndex) {
            for (var i = rowIndex - 1; i >= 0; --i) {
                if (this._$tableRows[i].hasClass('jtable-row-selected')) {
                    return i;
                }
            }

            return -1;
        },

        /* Search for a selected row (that is after given row index) to down and returns it's index 
        *************************************************************************/
        _findFirstSelectedRowIndexAfterIndex: function (rowIndex) {
            for (var i = rowIndex + 1; i < this._$tableRows.length; ++i) {
                if (this._$tableRows[i].hasClass('jtable-row-selected')) {
                    return i;
                }
            }

            return -1;
        },

        /* Makes row/rows 'selected'.
        *************************************************************************/
        _selectRows: function ($rows) {

            if (!this.options.multiselect) {
                this._deselectRows(this._getSelectedRows());
            }

            $rows.addClass('jtable-row-selected');
            if (this.options.selectingCheckboxes) {
                $rows.find('td.jtable-selecting-column input').attr('checked', true);
            }

            this._refreshSelectAllCheckboxState();
        },

        /* Makes row/rows 'non selected'.
        *************************************************************************/
        _deselectRows: function ($rows) {
            $rows.removeClass('jtable-row-selected');
            if (this.options.selectingCheckboxes) {
                $rows.find('td.jtable-selecting-column input').removeAttr('checked');
            }

            this._refreshSelectAllCheckboxState();
        },

        /* Updates state of the 'select/deselect' all checkbox according to count of selected rows.
        *************************************************************************/
        _refreshSelectAllCheckboxState: function () {
            if (!this.options.selectingCheckboxes || !this.options.multiselect) {
                return;
            }

            var totalRowCount = this._$tableRows.length;
            var selectedRowCount = this._getSelectedRows().length;

            if (selectedRowCount == 0) {
                this._$selectAllCheckbox.prop('indeterminate', false);
                this._$selectAllCheckbox.attr('checked', false);
            } else if (selectedRowCount == totalRowCount) {
                this._$selectAllCheckbox.prop('indeterminate', false);
                this._$selectAllCheckbox.attr('checked', true);
            } else {
                this._$selectAllCheckbox.attr('checked', false);
                this._$selectAllCheckbox.prop('indeterminate', true);
            }
        },

        /************************************************************************
        * EVENT RAISING METHODS                                                 *
        *************************************************************************/
        
        _onSelectionChanged: function () {
            this._trigger("selectionChanged", null, {});
        }

    });

})(jQuery);

/************************************************************************
* PAGING extension for jTable                                           *
*************************************************************************/
(function($) {

    //Reference to base object members
    var base = {
        load: $.hik.jtable.prototype.load,
        _create: $.hik.jtable.prototype._create,
        _createRecordLoadUrl: $.hik.jtable.prototype._createRecordLoadUrl,
        _addRowToTable: $.hik.jtable.prototype._addRowToTable,
        _removeRowsFromTable: $.hik.jtable.prototype._removeRowsFromTable,
        _onRecordsLoaded: $.hik.jtable.prototype._onRecordsLoaded
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {
            paging: false,
            pageSize: 10,

            messages: {
                pagingInfo: 'Showing {0} to {1} of {2} records'
            }
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _$pagingListArea: null, //Reference to the page list area in to bottom panel
        _totalRecordCount: 0, //Total count of records on all pages
        _currentPageNo: 1, //Current page number

        /************************************************************************
        * CONSTRUCTOR AND INITIALIZING METHODS                                  *
        *************************************************************************/

        /* Overrides base method to do paging-specific constructions.
        *************************************************************************/
        _create: function() {
            base._create.apply(this, arguments);
            this._createPageListArea();
        },

        /* Creates page list area if paging enabled.
        *************************************************************************/
        _createPageListArea: function() {
            if (!this.options.paging) {
                return;
            }

            this._$pagingListArea = $('<span class="jtable-page-list"></span>').appendTo(this._$bottomPanel);
        },

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides load method to set current page to 1.
        *************************************************************************/
        load: function() {
            this._currentPageNo = 1;
            base.load.apply(this, arguments);
        },

        /* Overrides _createRecordLoadUrl method to add paging info to URL.
        *************************************************************************/
        _createRecordLoadUrl: function() {
            var loadUrl = base._createRecordLoadUrl.apply(this, arguments);
            loadUrl = this._addPagingInfoToUrl(loadUrl, this._currentPageNo);
            return loadUrl;
        },

        /* Overrides _addRowToTable method to re-load table when a new row is created.
        *************************************************************************/
        _addRowToTable: function($tableRow, index, isNewRow) {
            if (isNewRow && this.options.paging) {
                this._reloadTable();
                return;
            }

            base._addRowToTable.apply(this, arguments);
        },

        /* Overrides _removeRowsFromTable method to re-load table when a row is removed from table.
        *************************************************************************/
        _removeRowsFromTable: function($rows, reason) {
            base._removeRowsFromTable.apply(this, arguments);

            if (this.options.paging) {
                if (this._$tableRows.length <= 0 && this._currentPageNo > 1) {
                    --this._currentPageNo;
                }

                this._reloadTable();
            }
        },

        /* Overrides _onRecordsLoaded method to to do paging specific tasks.
        *************************************************************************/
        _onRecordsLoaded: function(data) {
            this._totalRecordCount = data.TotalRecordCount;
            this._createPagingList();
            base._onRecordsLoaded.apply(this, arguments);
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Adds jtStartIndex and jtPageSize parameters to a URL as query string.
        *************************************************************************/
        _addPagingInfoToUrl: function(url, pageNumber) {
            if (!this.options.paging) {
                return url;
            }

            var jtStartIndex = (pageNumber - 1) * this.options.pageSize;
            var jtPageSize = this.options.pageSize;

            return (url + (url.indexOf('?') < 0 ? '?' : '&') + 'jtStartIndex=' + jtStartIndex + '&jtPageSize=' + jtPageSize);
        },

        /* Creates and shows the page list.
        *************************************************************************/
        _createPagingList: function() {
            if (!this.options.paging || this.options.pageSize <= 0) {
                return;
            }

            this._$pagingListArea.empty();
            var pageCount = this._calculatePageCount();

            this._createFirstAndPreviousPageButtons();
            this._createPageNumberButtons(this._calculatePageNumbers(pageCount));
            this._createLastAndNextPageButtons(pageCount);
            this._createPagingInfo();
            this._bindClickEventsToPageNumberButtons();
        },

        /* Creates and shows previous and first page links.
        *************************************************************************/
        _createFirstAndPreviousPageButtons: function() {
            if (this._currentPageNo > 1) {
                $('<span class="jtable-page-number-first">|&lt</span>')
                    .data('pageNumber', 1)
                    .appendTo(this._$pagingListArea);
                $('<span class="jtable-page-number-previous">&lt</span>')
                    .data('pageNumber', this._currentPageNo - 1)
                    .appendTo(this._$pagingListArea);
            }
        },

        /* Creates and shows next and last page links.
        *************************************************************************/
        _createLastAndNextPageButtons: function(pageCount) {
            if (this._currentPageNo < pageCount) {
                $('<span class="jtable-page-number-next">&gt</span>')
                    .data('pageNumber', this._currentPageNo + 1)
                    .appendTo(this._$pagingListArea);
                $('<span class="jtable-page-number-last">&gt|</span>')
                    .data('pageNumber', pageCount)
                    .appendTo(this._$pagingListArea);
            }
        },

        /* Creates and shows page number links for given number array.
        *************************************************************************/
        _createPageNumberButtons: function(pageNumbers) {
            var previousNumber = 0;
            for (var i = 0; i < pageNumbers.length; i++) {
                //Create "..." between page numbers if needed
                if ((pageNumbers[i] - previousNumber) > 1) {
                    $('<span class="jtable-page-number-space">...</span>').appendTo(this._$pagingListArea);
                }

                this._createPageNumberButton(pageNumbers[i]);
                previousNumber = pageNumbers[i];
            }
        },

        /* Creates a page number link and adds to paging area.
        *************************************************************************/
        _createPageNumberButton: function(pageNumber) {
            $('<span class="' + (this._currentPageNo == pageNumber ? 'jtable-page-number-active' : 'jtable-page-number') + '">' + pageNumber + '</span>')
                .data('pageNumber', pageNumber)
                .appendTo(this._$pagingListArea);
        },

        /* Calculates total page count according to page size and total record count.
        *************************************************************************/
        _calculatePageCount: function() {
            var pageCount = Math.floor(this._totalRecordCount / this.options.pageSize);
            if (this._totalRecordCount % this.options.pageSize != 0) {
                ++pageCount;
            }

            return pageCount;
        },

        /* Calculates page numbers and returns an array of these numbers.
        *************************************************************************/
        _calculatePageNumbers: function(pageCount) {
            if (pageCount <= 6) {
                //Show all pages
                var pageNumbers = [];
                for (var i = 1; i <= pageCount; ++i) {
                    pageNumbers.push(i);
                }

                return pageNumbers;
            } else {
                //show first three, last three, current, previous and next page numbers
                var shownPageNumbers = [1, 2, 3, pageCount - 2, pageCount - 1, pageCount];
                var previousPageNo = this._normalizeNumber(this._currentPageNo - 1, 1, pageCount, 1);
                var nextPageNo = this._normalizeNumber(this._currentPageNo + 1, 1, pageCount, 1);

                this._insertToArrayIfDoesNotExists(shownPageNumbers, previousPageNo);
                this._insertToArrayIfDoesNotExists(shownPageNumbers, this._currentPageNo);
                this._insertToArrayIfDoesNotExists(shownPageNumbers, nextPageNo);

                shownPageNumbers.sort(function(a, b) { return a - b; });
                return shownPageNumbers;
            }
        },

        /* Creates and shows paging informations.
        *************************************************************************/
        _createPagingInfo: function() {
            var startNo = (this._currentPageNo - 1) * this.options.pageSize + 1;
            var endNo = this._currentPageNo * this.options.pageSize;
            endNo = this._normalizeNumber(endNo, startNo, this._totalRecordCount, 0);

            if (endNo >= startNo) {
                var pagingInfoMessage = this._formatString(this.options.messages.pagingInfo, startNo, endNo, this._totalRecordCount);
                $('<span class="jtable-page-info">' + pagingInfoMessage + '</span>').appendTo(this._$pagingListArea);
            }
        },

        /* Binds click events of all page links to change the page.
        *************************************************************************/
        _bindClickEventsToPageNumberButtons: function() {
            var self = this;
            self._$pagingListArea
                .find('.jtable-page-number,.jtable-page-number-previous,.jtable-page-number-next,.jtable-page-number-first,.jtable-page-number-last')
                .click(function(e) {
                    e.preventDefault();
                    var $this = $(this);
                    self._currentPageNo = $this.data('pageNumber');
                    self._reloadTable();
                });
        }

    });

})(jQuery);

/************************************************************************
* SORTING extension for jTable                                          *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create,
        _normalizeFieldOptions: $.hik.jtable.prototype._normalizeFieldOptions,
        _createHeaderCellForField: $.hik.jtable.prototype._createHeaderCellForField,
        _createRecordLoadUrl: $.hik.jtable.prototype._createRecordLoadUrl
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {
            sorting: false,
            defaultSorting: ''
        },

        /************************************************************************
        * PRIVATE FIELDS                                                        *
        *************************************************************************/

        _lastSorting: '', //Last sorting of the table

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides _normalizeFieldOptions method to normalize sorting option for fields.
        *************************************************************************/
        _normalizeFieldOptions: function (fieldName, props) {
            base._normalizeFieldOptions.apply(this, arguments);
            props.sorting = (props.sorting != false);
        },

        /* Overrides _createHeaderCellForField to make columns sortable.
        *************************************************************************/
        _createHeaderCellForField: function (fieldName, field) {
            var $headerCell = base._createHeaderCellForField.apply(this, arguments);
            if (this.options.sorting && field.sorting) {
                this._makeColumnSortable($headerCell, fieldName);
            }

            return $headerCell;
        },

        /* Overrides _createRecordLoadUrl to add sorting specific info to URL.
        *************************************************************************/
        _createRecordLoadUrl: function () {
            var loadUrl = base._createRecordLoadUrl.apply(this, arguments);
            loadUrl = this._addSortingInfoToUrl(loadUrl);
            return loadUrl;
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Makes a column sortable.
        *************************************************************************/
        _makeColumnSortable: function ($columnHeader, fieldName) {
            var self = this;
            $columnHeader
                .addClass('jtable-column-header-sortable')
                .click(function(e) {
                    e.preventDefault();
                    self._sortTableByColumn($columnHeader);
                });

            //Default sorting?
            if (self.options.defaultSorting.indexOf(fieldName) > -1) {
                if (self.options.defaultSorting.indexOf('DESC') > -1) {
                    $columnHeader.addClass('jtable-column-header-sorted-desc');
                    self._lastSorting = fieldName + " DESC";
                } else {
                    $columnHeader.addClass('jtable-column-header-sorted-asc');
                    self._lastSorting = fieldName + " ASC";
                }
            }
        },

        /* Sorts table according to a column header.
        *************************************************************************/
        _sortTableByColumn: function ($columnHeader) {
            //Remove sorting styles from all columns except this one
            $columnHeader.siblings().removeClass('jtable-column-header-sorted-asc jtable-column-header-sorted-desc');

            //Sort ASC or DESC according to current sorting state
            if ($columnHeader.hasClass('jtable-column-header-sorted-asc')) {
                $columnHeader.removeClass('jtable-column-header-sorted-asc').addClass('jtable-column-header-sorted-desc');
                this._lastSorting = $columnHeader.data('fieldName') + " DESC";
            } else {
                $columnHeader.removeClass('jtable-column-header-sorted-desc').addClass('jtable-column-header-sorted-asc');
                this._lastSorting = $columnHeader.data('fieldName') + " ASC";
            }

            //Load current page again
            this._reloadTable();
        },

        /* Adds jtSorting parameter to a URL as query string.
        *************************************************************************/
        _addSortingInfoToUrl: function (url) {
            if (!this.options.sorting || this._lastSorting == '') {
                return url;
            }

            return (url + (url.indexOf('?') < 0 ? '?' : '&') + 'jtSorting=' + this._lastSorting);
        }

    });

})(jQuery);

/************************************************************************
* MASTER/CHILD tables extension for jTable                              *
*************************************************************************/
(function ($) {

    //Reference to base object members
    var base = {
        _removeRowsFromTable: $.hik.jtable.prototype._removeRowsFromTable
    };

    //extension members
    $.extend(true, $.hik.jtable.prototype, {

        /************************************************************************
        * DEFAULT OPTIONS / EVENTS                                              *
        *************************************************************************/
        options: {
            openChildAsAccordion: false
        },

        /************************************************************************
        * PUBLIC METHODS                                                        *
        *************************************************************************/
        
        /* Creates and opens a new child table for given row.
        *************************************************************************/
        openChildTable: function ($row, tableOptions, opened) {
            var self = this;

            //Show close button as default
            if (tableOptions.showCloseButton == undefined) {
                tableOptions.showCloseButton = true;
            }

            //Close child table when close button is clicked (default behavior)
            if (tableOptions.showCloseButton && !tableOptions.closeRequested) {
                tableOptions.closeRequested = function () {
                    self.closeChildTable($row);
                };
            }
            
            //If accordion style, close open child table (if it does exists)
            if (self.options.openChildAsAccordion) {
                $row.siblings().each(function () {
                    self.closeChildTable($(this));
                });
            }

            //Close child table for this row and open new one for child table
            self.closeChildTable($row, function () {
                var $childRowColumn = self.getChildRow($row).find('td').empty();
                var $childTableContainer = $('<div class="jtable-child-table-container"></div>').appendTo($childRowColumn);
                $childRowColumn.data('childTable', $childTableContainer);
                $childTableContainer.jtable(tableOptions);
                self.openChildRow($row);
                $childTableContainer.hide().slideDown('fast', function () {
                    if (opened) {
                        opened({ childTable: $childTableContainer });
                    }
                });
            });
        },

        /* Closes child table for given row.
        *************************************************************************/
        closeChildTable: function ($row, closed) {
            var self = this;

            var $childRowColumn = this.getChildRow($row).find('td');
            var $childTable = $childRowColumn.data('childTable');
            if (!$childTable) {
                if (closed) {
                    closed();
                }
                
                return;
            }

            $childRowColumn.data('childTable', null);
            $childTable.slideUp('fast', function () {
                $childTable.jtable('destroy');
                $childTable.remove();
                self.closeChildRow($row);
                if (closed) {
                    closed();
                }
            });
        },

        /* Returns a boolean value indicates that if a child row is open for given row.
        *************************************************************************/
        isChildRowOpen: function ($row) {
            return (this.getChildRow($row).is(':visible'));
        },

        /* Gets child row for given row, opens it if it's closed (Creates if needed).
        *************************************************************************/
        getChildRow: function ($row) {
            return $row.data('childRow') || this._createChildRow($row);
        },

        /* Creates and opens child row for given row.
        *************************************************************************/
        openChildRow: function ($row) {
            var $childRow = this.getChildRow($row);
            if (!$childRow.is(':visible')) {
                $childRow.show();
            }

            return $childRow;
        },

        /* Closes child row if it's open.
        *************************************************************************/
        closeChildRow: function ($row) {
            var $childRow = this.getChildRow($row);
            if ($childRow.is(':visible')) {
                $childRow.hide();
            }
        },

        /************************************************************************
        * OVERRIDED METHODS                                                     *
        *************************************************************************/

        /* Overrides _removeRowsFromTable method to remove child rows of deleted rows.
        *************************************************************************/
        _removeRowsFromTable: function ($rows, reason) {
            var self = this;

            if (reason == 'deleted') {
                $rows.each(function () {
                    var $row = $(this);
                    var $childRow = $row.data('childRow');
                    if ($childRow) {
                        self.closeChildTable($row);
                        $childRow.remove();
                    }
                });
            }

            base._removeRowsFromTable.apply(this, arguments);
        },

        /************************************************************************
        * PRIVATE METHODS                                                       *
        *************************************************************************/

        /* Creates a child row for a row, hides and returns it.
        *************************************************************************/
        _createChildRow: function ($row) {
            var index = this._findRowIndex($row);
            var totalColumnCount = this._$table.find('thead th').length;
            var $childRow = $('<tr class="jtable-child-row"></tr>').append('<td colspan="' + totalColumnCount + '"></td>');
            this._addRowToTable($childRow, index + 1);
            $row.data('childRow', $childRow);
            $childRow.hide();
            return $childRow;
        }
        
    });

})(jQuery);