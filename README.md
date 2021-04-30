# VR-Data-Visualization

### Background
This Unity project is intended to for visualizing multidimensional datasets in VR in the hopes that insights can be gained from "interacting" with a given dataset.

### Usage
1. Save your data in a CSV. Note that the data must be numerical with the exception of a potential row of strings for column names.
2. Locate the `readData.cs` script attached to the plane object in `SampleScene`. Drag your CSV onto the empty file object. 
3. In the same component, specify optional inital columns to visualize. If left blank, these will default to the first three columns in the CSV file.
4. Ensure that your VR headset is set up correctly. As of 4/30/21, this project is set up for Oculus Rift S.
5. Upon playing the scene, you should see your data moving around. This is because of the XR Grab Interactable script on the visualization.
6. Switch axes by pressing `u` or `i` on your keyboard as well as the primary buttons of your controllers. Try grabbing your data and moving it around!
