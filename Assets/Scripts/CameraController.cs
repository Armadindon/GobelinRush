namespace GoblinRush
{
	using UnityEngine;

	public class CameraController : SimpleGameStateObserver
	{
		[SerializeField] GameObject m_Target;
		Transform m_Transform;
		Vector3 m_InitPosition;

		[Header("Paramètres rotation caméra")]
		[Tooltip("Vitesse de rotation")]
		[SerializeField] private float speed;
		[Tooltip("Distance minimum")]
		[SerializeField] private float minZoom;
		[Tooltip("Distance maximum")]
		[SerializeField] private float maxZoom;

		private Camera m_mainCamera;

		void ResetCamera()
		{
			m_Transform.position = m_InitPosition;
		}

		protected override void Awake()
		{
			base.Awake();
			m_Transform = transform;
			m_InitPosition = m_Transform.position;

			m_mainCamera = this.GetComponent<Camera>();
        }

		void Update()
		{	
			//lorsque l'on clique sur une des flèches, la caméra pourra tourner ou zoomer/dézoomer
			//rotation
			if (Input.GetKey(KeyCode.RightArrow))
				transform.RotateAround(m_Target.transform.position, Vector3.down, speed * Time.deltaTime);
			if (Input.GetKey(KeyCode.LeftArrow))
				transform.RotateAround(m_Target.transform.position, Vector3.up, speed * Time.deltaTime);
			//zoom/dezoom
			if (Input.GetKey(KeyCode.UpArrow) && m_mainCamera.fieldOfView >= minZoom)
				m_mainCamera.fieldOfView -= speed * 0.01f;
			if (Input.GetKey(KeyCode.DownArrow) && m_mainCamera.fieldOfView <= maxZoom)
				m_mainCamera.fieldOfView += speed * 0.01f;
		}

		protected override void GameMenu(GameMenuEvent e)
		{
			ResetCamera();
		}
	}
}