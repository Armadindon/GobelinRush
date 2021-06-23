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
			//lorsque l'on appuie sur une des flèches, la caméra pourra tourner ou zoomer/dézoomer
			//rotation gauche
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
				transform.RotateAround(m_Target.transform.position, Vector3.down, speed * Time.deltaTime);
			//rotation droite
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
				transform.RotateAround(m_Target.transform.position, Vector3.up, speed * Time.deltaTime);
			//zoom
			if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)) && m_mainCamera.fieldOfView >= minZoom)
				m_mainCamera.fieldOfView -= speed * 0.01f;
			//dezoom
			if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && m_mainCamera.fieldOfView <= maxZoom)
				m_mainCamera.fieldOfView += speed * 0.01f;
		}

		protected override void GameMenu(GameMenuEvent e)
		{
			ResetCamera();
		}
	}
}