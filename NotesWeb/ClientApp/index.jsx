import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, browserHistory, IndexRoute } from 'react-router';
import { Link } from 'react-router';
import axios from 'axios';

var App = React.createClass({
    render: function () {
        return (
            <div>
                <h1>Simple SPA</h1>
                <div className="content">
                    {this.props.children}
                </div>
            </div>
        );
    }
});

var NotesList = React.createClass({
    getInitialState: function () {
        return {
            notes: []
        };
    },
    componentDidMount: function () {
        var _this = this;
        axios
            .get(notesApiAddress + '/api/notes/active/meta')
            .then(function(result) {
                result.data.sort(function(a, b) {
                    var dateA = Date.parse(a.creationDate);
                    var dateB = Date.parse(b.creationDate);
                    return dateA < dateB;
                });
                _this.setState({ notes: result.data });
            });
    },
    handleClick: function (id, e) {
        e.preventDefault();
        this.props.history.push("/noteDetails/" + id);
    },
    render: function () {
        var notes = this.state.notes;

        function createNotes(item) {
            return <a href="#" className="list-group-item col-sm-8" key={item.id} onClick={this.handleClick.bind(this, item.id)}>
                {item.title}
                <span className="badge float-right badge-dark">{item.creationDate}</span>
            </a>
        }

        var listItems = notes.map(createNotes, this);

        return (
            <div className="grid">
                <div className="list-group row">
                    {listItems}
                </div>
                <div className="row col-sm-4">
                    <Link className="btn btn-primary" role="button" to="/createNote">Создать заметку</Link>
                </div>
            </div>
        );
    }
});

var NoteDetails = React.createClass({
    getInitialState: function () {
        return {
            note: {}
        };
    },
    componentDidMount: function () {
        var _this = this;
        axios
            .get(notesApiAddress + '/api/notes/active/' + this.props.params.noteId)
            .then(function (result) { _this.setState({ note: result.data }); })
    },
    render: function () {
        var expirationDateSpecified = this.state.note.expirationDate != null;
        return (
            <form className="form-horizontal">
                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Заголовок:</label>
                    <div className="col-sm-8">
                        <input readOnly type="text" className="form-control form-control-lg" value={this.state.note.title}></input>
                    </div>
                </div>
                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Текст:</label>
                    <div className="col-sm-8">
                        <textarea readOnly className="form-control form-control-lg" rows="3" value={this.state.note.text}></textarea>
                    </div>
                </div>
                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Дата создания:</label>
                    <div className="col-sm-8">
                        <input readOnly type="text" className="form-control form-control-lg" value={this.state.note.creationDate}></input>
                    </div>
                </div>
                {expirationDateSpecified &&
                    <div className="form-group">
                        <label className="col-sm-2 font-weight-bold">Дата окончания:</label>
                        <div className="col-sm-8">
                            <input readOnly type="text" className="form-control form-control-lg" rows="3" value={this.state.note.expirationDate}></input>
                        </div>
                    </div>
                }
            </form>
        )
    }
});

var NoteCreationForm = React.createClass({
    createNote: function (e) {
        var _this = this;
        axios.post(notesApiAddress + '/api/notes/', {
            title: this._titleInput.value,
            text: this._contentInput.value,
            minutesToExpire: this._expirationInput.value
        }).then(function () {
            _this.props.history.push('/');
        });
        e.preventDefault();
    },

    render: function () {
        return (
            <form className="form-horizontal" onSubmit={this.createNote}>
                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Заголовок:</label>
                    <div className="col-sm-8">
                        <input ref={(a) => this
                            ._titleInput = a} type="text" className="form-control form-control-lg" placeholder=
                               "Введите название заметки"></input>
                    </div>
                </div>
                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Текст:</label>
                    <div className="col-sm-8">
                        <textarea ref={(a) => this
                            ._contentInput = a} className="form-control form-control-lg" rows="3" placeholder='Введите текст заметки'></textarea>
                    </div>
                </div>

                <div className="form-group">
                    <label className="col-sm-2 font-weight-bold">Прекращение срока действия:</label>
                    <div className="col-sm-3">
                        <select className="form-control" ref={(a) => this._expirationInput = a}>
                            <option value='0'>Никогда</option>
                            <option value='10'>10 минут</option>
                            <option value='60'>1 час</option>
                            <option value='1440'>1 день</option>
                            <option value='10080'>1 неделя</option>
                            <option value='312480'>1 месяц</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="col-sm-offset-2 col-sm-10">
                        <button type="submit" className="btn btn-default">Создать</button>
                    </div>
                </div>
            </form>
        );
    }
});

var destination = document.querySelector("#container");

ReactDOM.render(
    <Router>
        <Route path="/" component={App}>
            <IndexRoute component={NotesList} />
            <Route path="createNote" component={NoteCreationForm} />
            <Route path="noteDetails/:noteId" component={NoteDetails} />
        </Route>
    </Router>,
    destination
);
