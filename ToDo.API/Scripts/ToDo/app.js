
var ViewModel = function () {
    var self = this;

    // Used to define model properties which can notify the changes 
    // and update the model automatically.
    self.Id = ko.observable();
    self.Description = ko.observable("");
    self.IsDone = ko.observable(false);

    // Used to bind list of ToDo elements
    self.toDoList = ko.observableArray();
    self.isNotDoneList = ko.observableArray();

    // Uri-API address
    var toDoListUri = '/api/ToDoLists/';

    // jQuery’s Ajax helper method for creating, reading, updating and deleting.
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
    // GET / Show all toDoList objects (data).
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

        if (ToDoObject.Description != "" ) {
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
    };
    self.cancel = function () {
        self.clearFields();
        $('#Save').show();
        $('#Clear').show();
        $('#Update').hide();
        $('#Cancel').hide();
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

    //////////////////////////////////////////////////////////////
    //DELETE / Remove an toDoList object from the list and database
    //////////////////////////////////////////////////////////////
    self.deleteToDo = function (toDo) {
        if (confirm('Are you sure to Delete this item (ID=' + toDo.Id + ')?')) {
            ajaxFunction(toDoListUri + toDo.Id, 'DELETE').done(function () {
                getToDoList();
            })
        }
    }

    getToDoList();
};
ko.applyBindings(new ViewModel());
