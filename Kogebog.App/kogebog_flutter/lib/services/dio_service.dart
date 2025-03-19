import 'package:dio/dio.dart';

class DioService {
  static final Dio _dio = Dio(BaseOptions(
    baseUrl: 'http://10.0.2.2:5023/api',
    headers: {
      "Content-Type": "application/json",
    },
  ));

  static Dio get dio => _dio;
}
