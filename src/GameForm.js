import React, { useState } from "react";
import { Form, Button, Container, Row, Col } from "react-bootstrap";
import axios from "axios";

const GameForm = () => {
  const initialTeamState = {
    TeamName: "",
    NoOfParticipants: 0,
    Participants: []
  };

  const [gameName, setGameName] = useState("");
  const [noOfTeams, setNoOfTeams] = useState(0);
  const [teams, setTeams] = useState([]);

  const deepClone = (obj) => JSON.parse(JSON.stringify(obj));

  const handleInputChange = (index, field, value) => {
    const updatedTeams = deepClone(teams);
    updatedTeams[index][field] = value;
    setTeams(updatedTeams);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Check if all teams have the same number of participants
    const numParticipants = teams[0].Participants.length;
    const isSameParticipants = teams.every(team => team.Participants.length === numParticipants);

    if (!isSameParticipants) {
      alert("Number of participants in each team should be the same. Please enter the data again.");
      return;
    }

    // Prepare the data object to be sent
    const postData = {
      GameName: gameName,
      NoOfTeams: noOfTeams,
      Teams: teams
    };

    // Send POST request using Axios
    axios.post("http://localhost:53857/api/GamePlanner1/PostGame", postData)
      .then(response => {
        console.log("Response:", response.data);
        alert("Game details added successfully!");
      })
      .catch(error => {
        console.error("Error:", error);
        alert("Error in adding game details. Please try again.");
      });
  };

  const handleNoOfTeamsChange = (e) => {
    const count = parseInt(e.target.value) || 0;
    setNoOfTeams(count);

    const initialTeams = Array.from({ length: count }, () => deepClone(initialTeamState));
    setTeams(initialTeams);
  };

  const handleReset = () => {
    setGameName("");
    setNoOfTeams(0);
    setTeams([]);
  };

  return (
    <Container>
      <Row className="justify-content-center">
        <Col md={8}>
          <h1 className="text-center mb-4">Game Form</h1>
          <Form onSubmit={handleSubmit}>
            <Form.Group controlId="gameName">
              <Form.Label>Game Name:</Form.Label>
              <Form.Control
                type="text"
                value={gameName}
                onChange={(e) => setGameName(e.target.value)}
                placeholder="Enter Game Name"
                required
              />
            </Form.Group>

            <Form.Group controlId="noOfTeams">
              <Form.Label>No of Teams:</Form.Label>
              <Form.Control
                type="number"
                value={noOfTeams}
                onChange={handleNoOfTeamsChange}
                min="0"
                placeholder="Enter No of Teams"
                required
              />
            </Form.Group>

            {teams.map((team, teamIndex) => (
              <div key={teamIndex}>
                <h3>Team {teamIndex + 1}</h3>
                <Form.Group controlId={`teamName${teamIndex}`}>
                  <Form.Label>Team Name:</Form.Label>
                  <Form.Control
                    type="text"
                    value={team.TeamName}
                    onChange={(e) => handleInputChange(teamIndex, "TeamName", e.target.value)}
                    placeholder="Team Name"
                    required
                  />
                </Form.Group>

                <Form.Group controlId={`noOfParticipants${teamIndex}`}>
                  <Form.Label>No of Participants:</Form.Label>
                  <Form.Control
                    type="number"
                    value={team.NoOfParticipants}
                    onChange={(e) => handleInputChange(teamIndex, "NoOfParticipants", parseInt(e.target.value) || 0)}
                    placeholder="No of Participants"
                    required
                  />
                </Form.Group>

                {[...Array(team.NoOfParticipants)].map((_, participantIndex) => (
                  <Form.Group key={participantIndex} controlId={`participantName${teamIndex}-${participantIndex}`}>
                    <Form.Label>{`Participant ${participantIndex + 1} Name:`}</Form.Label>
                    <Form.Control
                      type="text"
                      value={team.Participants[participantIndex] || ""}
                      onChange={(e) => {
                        const updatedTeams = deepClone(teams);
                        updatedTeams[teamIndex].Participants[participantIndex] = e.target.value;
                        setTeams(updatedTeams);
                      }}
                      placeholder={`Participant ${participantIndex + 1} Name`}
                      required
                    />
                  </Form.Group>
                ))}
              </div>
            ))}

            <Button variant="primary" type="submit" className="mt-3 mr-3">
              Submit
            </Button>
            <Button variant="secondary" type="button" onClick={handleReset} className="mt-3">
              Reset
            </Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default GameForm;

