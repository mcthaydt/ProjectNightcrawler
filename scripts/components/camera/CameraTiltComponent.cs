using Godot;

public partial class CameraTiltComponent : Node
{
	[Export] public Resource CameraData; 
	
	[Export] public NodePath CameraHorizontalTiltGimbalNodePath;
	private Node3D _cameraHorizontalTiltGimbal;
	
	[Export] public NodePath CameraVerticalTiltGimbalNodePath;
	private Node3D _cameraVerticalTiltGimbal;

	[Export] public float MaxCameraTiltDegree = 20.0f;
	[Export] public float TimeBeforeTiltStarts = .05f;
	
	private float _cameraTiltLerpSpeed = 0.0f;
	
	private double _keyPressTime = 0.0f;
	private double _keyPressCooldownTime = 0.0f;
	
	public override void _Ready()
	{
		if (CameraHorizontalTiltGimbalNodePath != null)
		{
			_cameraHorizontalTiltGimbal = GetNode<Node3D>(CameraHorizontalTiltGimbalNodePath);
		}
		
		if (CameraVerticalTiltGimbalNodePath != null)
		{
			_cameraVerticalTiltGimbal = GetNode<Node3D>(CameraVerticalTiltGimbalNodePath);
		}
	}
	
	public override void _Process(double delta)
	{
		float currentTiltAmountHorizontal = 0.0f;
		float currentTiltAmountHorizontalDeg2Rad = 0.0f;
		float currentTiltAmountVertical = 0.0f;
		float currentTiltAmountVerticalDeg2Rad = 0.0f;
		Vector3 currentCameraHorizontalTiltGimbalRotation = _cameraHorizontalTiltGimbal.Rotation;
		Vector3 currentCameraVerticalTiltGimbalRotation = _cameraVerticalTiltGimbal.Rotation;

		if (CameraData is BaseCameraData baseCameraData)
		{
			_cameraTiltLerpSpeed = baseCameraData.TiltLerpSpeed;
		}
		else
		{
			GD.Print("Missing Camera Data!");
			return;
		}

		if (Input.IsActionPressed("move_down") || Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")
		    || Input.IsActionPressed("move_up"))
		{
			_keyPressTime += delta;
		}
		else
		{
			_keyPressCooldownTime += delta;
			if (_keyPressCooldownTime > TimeBeforeTiltStarts / 1.3f)
			{
				 _keyPressTime = 0.0f;
				 _keyPressCooldownTime = 0.0f;
			}
		}

		if (_keyPressTime > TimeBeforeTiltStarts)
		{
			if (Input.IsActionPressed("move_right"))
			{
				currentTiltAmountHorizontal = MaxCameraTiltDegree;
			}
			else if (Input.IsActionPressed("move_left"))
			{
				currentTiltAmountHorizontal = -MaxCameraTiltDegree;
			}
			else
			{
				currentTiltAmountHorizontal = 0.0f;
			}
		  
			if (Input.IsActionPressed("move_up"))
			{
				currentTiltAmountVertical = MaxCameraTiltDegree / 2.0f;
			}
			else if (Input.IsActionPressed("move_down"))
			{
				currentTiltAmountVertical = -MaxCameraTiltDegree / 2.0f;
			}
			else
			{
				currentTiltAmountVertical = 0.0f;
			}
		}
	
		currentTiltAmountHorizontalDeg2Rad = Mathf.DegToRad(currentTiltAmountHorizontal);
		currentTiltAmountVerticalDeg2Rad = Mathf.DegToRad(currentTiltAmountVertical);

		currentCameraHorizontalTiltGimbalRotation.Z = Mathf.LerpAngle(currentCameraHorizontalTiltGimbalRotation.Z, currentTiltAmountHorizontalDeg2Rad, (float)delta * _cameraTiltLerpSpeed);
		currentCameraVerticalTiltGimbalRotation.X = Mathf.LerpAngle(currentCameraVerticalTiltGimbalRotation.X, currentTiltAmountVerticalDeg2Rad, (float)delta * _cameraTiltLerpSpeed);
		
		_cameraHorizontalTiltGimbal.Rotation = currentCameraHorizontalTiltGimbalRotation;
		_cameraVerticalTiltGimbal.Rotation = currentCameraVerticalTiltGimbalRotation;
	}
}
