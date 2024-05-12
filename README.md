# Boids
Boids is the term for simulating the flocking behavior of birds. It's also observed in some species fish.

This simulation or concept is very popular since it's possible to create complex looking behavior with very few rules.<br><br>
The rules are the following:

<h3>Separation</h3>
Boids try to maintain a minimum distance towards each other to avoid crowding. If to close to another, they steer away.
<br>
<h3>Alignment</h3>
Boids try to match the direction and speed of nearby boids. They steer to align themselves with the average direction of their neighbors.
<br>
<h3>Cohesion</h3>
Boids try to stay close to the center of mass of nearby boids. They steer to move toward the average position of their neighbors.
<br><br><br><br>
This is a simplified 2D project and here are some insights of the internal workings:
<br><br>
<ul>
 <li>The Simulation takes place in the camera view and the boids are just simple triangles</li>
 <li>Since finding neighbors is mandatory, there needs to be a time and memory efficient way of doing it, because the complexity gets multiplied by the number of boids and this can get bad very quickly. Therefore, i chose a grid layout to determine the neighbors and it works like this: 
    <ul>
     <li>Each cell of the grid is calculated based on a given cellSize and the maxBounds on the x-axis and y-axis and the cells are stored in a dictionary. The important part is, that only the cell that the boid is inside of, is added to the dictionary
     </li>
     <li>For each boid, the 8 surrounding tiles are scanned and if any of these cells are already in the dictionary, it means that there is a boid on it</li>
    </ul> 
  <li>...</li>
</ul>
<br>
Visualization of view angle and neighbor-detection<br>

![](https://github.com/rtatlisu/Boids/blob/main/BoidGif1.gif)









