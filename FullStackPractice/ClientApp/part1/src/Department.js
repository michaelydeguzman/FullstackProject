import React, {Component} from 'react';
import {Table} from 'react-bootstrap';

export class Department extends Component {

    constructor(props) {
        super(props);
        this.state={
            deps:[]
        }


    }

    refreshList() {
        fetch(process.env.REACT_APP_API+'Departments')
        .then(response=>response.json())
        .then(data=>{
            this.setState({deps:data});

        })
    }

    componentDidMount(){
        this.refreshList();
    }

    render() {
        const {deps} = this.state;

        return (
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <td>Deparment Id</td>
                            <td>Department Name</td>
                            <td>Options</td>
                        </tr>
                    </thead>
                    <tbody>
                        {deps.map(dep=>
                                <tr key={dep.DeparmentId}>
                                    <td>{dep.DepartmentId}</td>
                                    <td>{dep.DepartmentName}</td>
                                    <td>Edit / Delete</td>
                                </tr>
                            )
                        }
                    </tbody>
                </Table>
            </div>
        )
    }
}