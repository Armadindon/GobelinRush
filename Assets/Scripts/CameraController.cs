namespace GoblinRush
{
	using UnityEngine;

	public class CameraController : SimpleGameStateObserver
	{
		[SerializeField] GameObject m_Target;
		Transform m_Transform;
		Vector3 m_InitPosition;

		[Header("Paramètres rotation caméra")]
		//[Tooltip("Caméra")]
		//[SerializeField] private Camera m_Camera;
		[Tooltip("Vitesse de rotation")]
		[SerializeField] private float speed;
		[Tooltip("Distance minimum")]
		[SerializeField] private float minZoom;
		[Tooltip("Distance maximum")]
		[SerializeField] private float maxZoom;

		private Camera mainCamera;

		void ResetCamera()
		{
			m_Transform.position = m_InitPosition;
		}

		protected override void Awake()
		{
			base.Awake();
			m_Transform = transform;
			m_InitPosition = m_Transform.position;

			mainCamera = this.GetComponent<Camera>();
        }

		void Update()
		{
			//if (!GameManager.Instance.IsPlaying) return;

			if (Input.GetKey(KeyCode.RightArrow))
				transform.RotateAround(m_Target.transform.position, Vector3.down, 50 * Time.deltaTime);
			if (Input.GetKey(KeyCode.LeftArrow))
				transform.RotateAround(m_Target.transform.position, Vector3.up, 50 * Time.deltaTime);
			if (Input.GetKey(KeyCode.UpArrow) && mainCamera.fieldOfView >= minZoom)
				mainCamera.fieldOfView -= 0.5f;
			if (Input.GetKey(KeyCode.DownArrow) && mainCamera.fieldOfView <= maxZoom)
				mainCamera.fieldOfView += 0.5f;
		}

		protected override void GameMenu(GameMenuEvent e)
		{
			ResetCamera();
		}
	}
}