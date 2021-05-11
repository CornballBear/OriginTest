import React, { useEffect, useState } from "react";
import {
  NavDropdown,
  Navbar,
  Nav,
  Table,
  Form,
  Button,
} from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import axios from "axios";

function App() {
  const [cities, setCities] = useState([]);
  const [selectedCity, setSelectedCity] = useState([]);
  const [isEdit, setIsEdit] = useState(false);

  //todo: get data from the backend api
  useEffect(() => {
    axios.get("https://localhost:5001/city").then((response) => {
      setCities(response.data);
    });
  });

  //todo: change to adding city page
  const openAdd = () => {
    setIsEdit(true);
  };

  //todo: Handle submit function while adding new city
  const handleSubmit = (event) => {
    setIsEdit(false);
    setSelectedCity([]);
    var name = event.target.formGridName.value;
    var temperature = event.target.formGridTemp.value;
    fetch("https://localhost:5001/city/add", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: name,
        temperature: temperature,
      }),
    });
  };

  //todo: show all the information
  const showAllCities = () => {
    setSelectedCity([]);
    setIsEdit(false);
  };

  //todo: show the information of the selected city
  const selectCity = (event) => {
    setSelectedCity(
      cities.filter((city) =>
        city.name.includes(event.target.getAttribute("value"))
      )
    );
    setIsEdit(false);
  };

  return (
    <div className="App">
      <Navbar bg="dark" variant="dark" expand="lg">
        <Navbar.Brand onClick={showAllCities}>Origin Test</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link onClick={openAdd}>
              Add
            </Nav.Link>
            <NavDropdown title="Select City" id="basic-nav-dropdown">
              <NavDropdown.Item onClick={showAllCities}>
                Show All Cities
              </NavDropdown.Item>

              {cities.map((city) => {
                return (
                  <NavDropdown.Item
                    name={city.name}
                    value={city.name}
                    eventKey={city.name}
                    onClick={selectCity}
                  >
                    {city.name}
                  </NavDropdown.Item>
                );
              })}
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Navbar>

      <div>
        {isEdit ? (
          <div class="centerDiv">
            <Form onSubmit={handleSubmit}>
              <Form.Group controlId="formGridName">
                <Form.Label>Name</Form.Label>
                <Form.Control />
              </Form.Group>

              <Form.Group controlId="formGridTemp">
                <Form.Label>Temperature</Form.Label>
                <Form.Control />
              </Form.Group>
              <Button variant="dark" type="submit">
                Submit
              </Button>
            </Form>
          </div>
        ) : (
          <Table striped bordered hover variant="light">
            <thead>
              <tr>
                <th>Name</th>
                <th>Temperature</th>
              </tr>
            </thead>
            <tbody>
              {selectedCity.length === 0
                ? cities.map((city) => (
                    <tr>
                      <td>{city.name}</td>
                      <td>{city.temperature}</td>
                    </tr>
                  ))
                : selectedCity.map((city) => (
                    <tr>
                      <td>{city.name}</td>
                      <td>{city.temperature}</td>
                    </tr>
                  ))}
            </tbody>
          </Table>
        )}
      </div>
    </div>
  );
}

export default App;
