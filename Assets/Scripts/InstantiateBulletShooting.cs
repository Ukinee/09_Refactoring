using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private Transform _objectToShoot;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _number;
    [SerializeField] private float _timeWaitShooting;

    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(_timeWaitShooting);

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ShootingWorker());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator ShootingWorker()
    {
        bool isWork = enabled;

        while (isWork)
        {
            Vector3 direction = (_objectToShoot.position - transform.position).normalized;
            Rigidbody newBullet = Instantiate<Rigidbody>(_prefab, transform.position + direction, Quaternion.identity);

            newBullet.transform.up = direction;
            newBullet.velocity = direction * _number;

            yield return _waitForSeconds;
        }
    }
}
