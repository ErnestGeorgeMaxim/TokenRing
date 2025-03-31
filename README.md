# Token Ring Network Simulation
This project simulates the activity of a Token Ring network with 10 computers. The token moves in one direction, allowing message transmission between randomly chosen source and destination computers.

## Key Features:
- Each computer has a unique IP address and a buffer.
- A single token circulates, carrying the source IP, destination IP, message, and status flags.
- At each step, a random source and destination are selected.
- The token moves through the network until it reaches its destination.
- The destination stores the message, and the token continues back to the source.
- The source resets the token, making it available for the next transmission.
- The simulation runs for 10 steps, displaying each token movement.
