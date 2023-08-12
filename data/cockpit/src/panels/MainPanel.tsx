import React from "react";
import { Container, Box, Typography, Paper } from "@mui/material";
import ControlButton from "./ControlButton";

export default () => {
  return (
    <Container maxWidth="sm">
      <Paper
        elevation={3}
        sx={{
          mt: 8,
          px: 2,
          py: 4,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h4">
          COCKPIT
        </Typography>
        <Box>
          <ControlButton
            name="test"
            text="Test"
            apiUrl="/api/script/test01.sh"
          />
          <ControlButton
            name="Backup"
            text="MongoDB Backup."
            apiUrl="/api/script/backup_mongodb.sh"
          />
          <ControlButton
            name="Restore"
            text="MongoDB Restore. (latest version.)"
            apiUrl="/api/script/restore_mongodb.sh"
          />
        </Box>
      </Paper>
    </Container>
  );
};
