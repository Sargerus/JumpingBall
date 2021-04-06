using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayersCollider : MonoBehaviour, ISetupObject
{
    private SphereCollider _sc;
    private Player _parentScript;
    //private Rigidbody _parentRG;
    private Vector3 force;
    //private Vector3 _lastCreated = Vector3.zero;

    private void Awake()
    {
        force = new Vector3(0, 3.5f, 0f);// 0.3f);
    }

    public void SetupObject()
    {
        CheckComponentsReferences();
        CheckColliderSetup();
        //_parentRG.AddForce(force, ForceMode.Impulse);
    }

    private void CheckColliderSetup()
    {
        _sc.isTrigger = true;
    }

    private void CheckComponentsReferences()
    {
        if(!_sc)
            _sc = GetComponent<SphereCollider>();

        if (!_parentScript)
            _parentScript = transform.parent.GetComponent<Player>();
        //if (!_parentRG)
        //{
        //    var parentsScript = transform.parent.GetComponent<Player>();
        //    if (parentsScript)
        //        _parentRG = parentsScript._rg;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Platform":
                if (_parentScript)
                    _parentScript.MoveUp(other.transform);
            //    //GameObject asd = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            //    //asd.transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
            //    //if(_lastCreated != Vector3.zero)
            //    //{
            //    //    Debug.Log(Vector3.Distance(asd.transform.position, _lastCreated));
            //    //}
            //    //_lastCreated = asd.transform.position;
            //    //_parentRG.AddForce(force, ForceMode.Impulse);
                break;
            //
            //case "UpJumpBound":
            //    if (_parentScript)
            //        _parentScript.MoveDown();
            //    break;
        }
    }

}
