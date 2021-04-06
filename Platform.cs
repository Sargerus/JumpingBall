using UnityEngine;

class Platform : MonoBehaviour
{
    private Material _touchedMaterial;
    private Material _untouchedMaterial;
    private MeshRenderer _msh;
    private PlatformsManager _parentScripts;
    private ParticleSystem _particle;
    private float _speed;

    public void SetSpeed(float value) => _speed = value;
    public float GetSpeed() => _speed;

    private void Awake()
    {
        _touchedMaterial = (Material)Resources.Load("Material/TouchedPlane", typeof(Material));
        _untouchedMaterial = (Material)Resources.Load("Material/UntouchedPlane", typeof(Material));
        _parentScripts = transform.parent.GetComponent<PlatformsManager>();
        _msh = GetComponent<MeshRenderer>();
        _particle = transform.GetChild(0).GetComponent<ParticleSystem>();

        if (_msh)
        {
            _msh.material = _untouchedMaterial;
            _msh.receiveShadows = false;
            _msh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
        
    }

    private void Start()
    {
        if (_particle)
            _particle.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayersTrigger"))
        {
            //_msh.material = _touchedMaterial;
            if (_particle)
                _particle.Play();
        }
            
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, - _speed) * Time.fixedDeltaTime);
        if (transform.position.z <= -13f)
        {
            //if (_msh)
            //    _msh.material = _untouchedMaterial;

            if (_parentScripts)
                _parentScripts.MovePlatformWhichColliderWithDeadLine(transform);

            if (_particle)
                _particle.Stop();
        }
    }
}
