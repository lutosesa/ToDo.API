
var ViewModel = function () {
    var self = this;

    // Uri-API address
    var toDoListUri = '/api/ToDoLists/';

    // Headline for the column/field "IsDone"
    var seletedHeadlines = ['All Items', 'Not Done', 'Done']

    // Used to define model properties which can notify the changes 
    // and update the model automatically.
    self.Id = ko.observable();
    self.Description = ko.observable('');
    self.IsDone = ko.observable(false);

    // Used to bind list of ToDoList items
    self.toDoList = ko.observableArray();
   
    self.selectedItems = ko.observableArray(seletedHeadlines);
    self.selected = ko.observable('');

    /////////////////////////////////////////////////////////////////////
    // jQuery’s Ajax function for requests to create-POST, read-GET, 
    // update-PUT and delete-DELETE.
    /////////////////////////////////////////////////////////////////////
    function ajaxFunction(uri, method, data) { 
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Error : ' + errorThrown);
        });
    }

    //////////////////////////////////////////////////////////////
    // GET / Show all toDoList objects/items (data).
    //////////////////////////////////////////////////////////////
    function getToDoList() {
        ajaxFunction(toDoListUri, 'GET').done(function (data) {
            self.toDoList(data);
        });
    }

    //////////////////////////////////////////////////////////////
    // Clear Fields
    //////////////////////////////////////////////////////////////
    self.clearFields = function clearFields() {
        self.Description('');
        self.IsDone(false);
    }

    //////////////////////////////////////////////////////////////
    // POST / add new ToDoList object to the database and List
    //////////////////////////////////////////////////////////////
    self.addNewToDo = function () {
        var ToDoObject = {
            Id: self.Id(),
            Description: self.Description(),
            IsDone: self.IsDone()
        };

        if (ToDoObject.Description != '' ) {
            ajaxFunction(toDoListUri, 'POST', ToDoObject).done(function () {
                self.clearFields();
                alert('ToDo Added Successfully !');
                //self.toDoList.push(ToDoObject);
                getToDoList()
            });
        }
        else {
            alert('Please Enter a value in the field "Thing to Do" for adding!!');
        }
    }

    //////////////////////////////////////////////////////////////
    // Get Detail when selecting item for updating
    //////////////////////////////////////////////////////////////
    self.detailToDo = function (selectedToDo) {
        self.Id(selectedToDo.Id);
        self.Description(selectedToDo.Description);
        self.IsDone(selectedToDo.IsDone);
        $('#Save').hide();
        $('#Clear').hide();
        $('#Update').show();
        $('#Cancel').show();
        $('#IsDone-group').show();
    };
    self.cancel = function () {
        self.clearFields();
        $('#Save').show();
        $('#Clear').show();
        $('#Update').hide();
        $('#Cancel').hide();
        $('#IsDone-group').hide();
    }

    //////////////////////////////////////////////////////////////
    //PUT / UPDATE an toDolist object 
    //////////////////////////////////////////////////////////////
    self.updateToDo = function () {
        var ToDoObject = {
            Id: self.Id(),
            Description: self.Description(),
            IsDone: self.IsDone()
        };
        if (ToDoObject.Description != "") {
            ajaxFunction(toDoListUri + self.Id(), 'PUT', ToDoObject).done(function () {
                alert('ToDo Updated Successfully !');
                getToDoList();
                self.cancel();
            });
        }
        else {
            alert('Please Enter a value in the field "Thing to Do" for updating!!');
        }
    }

    /////////////////////////////////////////////////////////////////
    //DELETE / Remove an toDoList object from the list and database
    /////////////////////////////////////////////////////////////////
    self.deleteToDo = function (toDo) {
        if (confirm('Are you sure to Delete this item (ID=' + toDo.Id + ')?')) {
            ajaxFunction(toDoListUri + toDo.Id, 'DELETE').done(function () {
                getToDoList();
            })
        }
    }

    ////////////////////////////////////////////////////////
    // Filter the list of items by 'IsDone' field/property
    ////////////////////////////////////////////////////////
    self.filteredIsDoneItems = ko.computed(function () {
        var filter = self.selected();
        if (!filter || filter == 'All Items') {
            return self.toDoList();
        } else if (filter == 'Not Done') {
            return ko.utils.arrayFilter(self.toDoList(), function (i) {
                return i.IsDone == false;
            });
        } else {
            return ko.utils.arrayFilter(self.toDoList(), function (i) {
                return i.IsDone == true;
            });
        }
    });
    
    getToDoList();
};
ko.applyBindings(new ViewModel());
