using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

      

    enum BallState
    {
        Start,
        Moving,
        End
    }

    [SerializeField] string jsonFileName = "ball_path";
	[SerializeField] public float speed;
	[SerializeField] public bool inFocus;



	private MeshRenderer _meshRenderer;

	private BallState _state = BallState.Start;
	private Vector3[] _positions = new Vector3[0];
	private LineRenderer _trace;
	private float _traceLength;


	void Awake()
	{
        
		_meshRenderer = GetComponent<MeshRenderer>();
		_trace = GetComponent<LineRenderer>();
	}



	public void SetPositions(Vector3[] positions)
	{
		_positions = positions;
		_state = BallState.Start;
		if (positions.Length != 0)
			transform.position = positions[0];
		_trace.SetPositions(positions);
		_trace.positionCount = 0;
		_traceLength = 0;
		for (int i = 0; i < positions.Length - 1; i++)
		{
			_traceLength += (positions[i] - positions[i + 1]).magnitude;
		}
	}



	

	IEnumerator MoveBall()
	{
		_state = BallState.Moving;
		int i0 = 0, i1 = 0;
		float t = 0;

		while (true)
		{
			i0 = (int)Mathf.Floor(t);
			i1 = (int)Mathf.Ceil(t);


			if (i0 < 0 || i1 >= _positions.Length)
			{
				break;

			}

			else
			{
				transform.position = Vector3.Lerp(_positions[i0], _positions[i1], t - Mathf.Floor(t));
				_trace.positionCount = i1 + 1;
				_trace.SetPosition(i1, transform.position);

				t += Time.deltaTime * speed * 5 / _traceLength * _positions.Length;
				yield return new WaitForEndOfFrame();
			}
		}

		_state = BallState.End;
	}



    public void OnDoubleClick()
    {
	    if (_state != BallState.Start)
	    {
		    _state = BallState.Start;
		    transform.position = _positions[0];
		    _trace.positionCount = 0;
		    StopAllCoroutines();
	    }

    }

    public void OnClick()
    {
	    if (_state != BallState.Moving && inFocus)
	    {
		    StartCoroutine(MoveBall());
	    }
	    else
	    {
		    speed = Mathf.Max(speed, 1);
	    }
    }

    public void SelectedMaterial()
    {
    
        if (inFocus)
        {
			_meshRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
			_meshRenderer.material.SetColor("_Color", Color.blue);
        }
    }

     

}