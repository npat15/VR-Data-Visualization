# VR-Data-Visualization

### Background
This Unity project is intended to for visualizing multidimensional datasets in VR in the hopes that insights can be gained from "interacting" with a given dataset.

### Usage
1. Save your data in a CSV. Note that the data must be numerical with the exception of a potential row of strings for column names.
2. Locate the `readData.cs` script attached to the plane object in `SampleScene`. Drag your CSV onto the empty file object. 
![ea023c07e2ae1577b4cf7fd59f8571ca](https://user-images.githubusercontent.com/59075610/116740495-f29ee980-a9a9-11eb-9684-6b32f8d64bdb.png)

4. In the same component, specify optional inital columns to visualize. If left blank, these will default to the first three columns in the CSV file.
5. Ensure that your VR headset is set up correctly. As of 4/30/21, this project is set up for Oculus Rift S.
6. Upon playing the scene, you should see your data moving around. This is because of the XR Grab Interactable script on the visualization.
7. Switch axes by pressing `u` or `i` on your keyboard as well as the primary buttons of your controllers. Try grabbing your data and moving it around!
