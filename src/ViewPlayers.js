import React, { useState, useEffect } from 'react';
import { Table, Form, Button, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';

const ViewPlayers = () => {
  const [gameName, setGameName] = useState('');
  const [gameNames, setGameNames] = useState([]);
  const [teams, setTeams] = useState([]);
  const [selectedTeam, setSelectedTeam] = useState('');
  const [players, setPlayers] = useState([]);

  useEffect(() => {
    fetchGameNames();
  }, []);

  const fetchGameNames = () => {
    axios.get('http://localhost:53857/api/GamePlanner1/GetAllGameNames')
      .then(response => {
        setGameNames(response.data);
      })
      .catch(error => {
        console.error('Error fetching game names:', error);
      });
  };

  const fetchTeams = (selectedGame) => {
    axios.get(`http://localhost:53857/api/GamePlanner1/GetTeams/${selectedGame}`)
      .then(response => {
        setTeams(response.data);
      })
      .catch(error => {
        console.error('Error fetching teams:', error);
      });
  };

  const fetchPlayers = (selectedTeam) => {
    axios.get(`http://localhost:53857/api/GamePlanner1/Getplayers/${selectedTeam}`)
      .then(response => {
        setPlayers(response.data);
      })
      .catch(error => {
        console.error('Error fetching players:', error);
      });
  };

  const handleGameChange = (e) => {
    const selectedGame = e.target.value;
    setGameName(selectedGame);
    fetchTeams(selectedGame);
  };

  const handleTeamChange = (e) => {
    const selectedTeam = e.target.value;
    setSelectedTeam(selectedTeam);
    fetchPlayers(selectedTeam);
  };

  return (
    <Container className="mt-5">
      <Form className="border p-4 rounded">
        <Row className="mb-3">
          <Col xs={4}>
            <select
              value={gameName}
              onChange={handleGameChange}
              className="form-select"
              style={{ width: '100%' }}
            >
              <option value="">Select a Game</option>
              {gameNames.map((game, index) => (
                <option key={index}>{game}</option>
              ))}
            </select>
          </Col>
          <Col xs={4}>
            <select
              value={selectedTeam}
              onChange={handleTeamChange}
              className="form-select"
              style={{ width: '100%' }}
              disabled={!gameName}
            >
              <option value="">Select a Team</option>
              {teams.map((team, index) => (
                <option key={index}>{team}</option>
              ))}
            </select>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover responsive="md">
              <thead>
                <tr>
                  <th>Player Name</th>
                </tr>
              </thead>
              <tbody>
                {players.map((player, index) => (
                  <tr key={index}>
                    <td>{player}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Col>
        </Row>
      </Form>
    </Container>
  );
};

export default ViewPlayers;
